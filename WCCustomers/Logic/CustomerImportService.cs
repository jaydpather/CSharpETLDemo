using Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    //todo: this should also be an internal class, accessed through a factory.
    //that way, you can unit test the caller
    //(the caller should not be the entry point of the app)
    public class CustomerImportService
    {
        private ICustomerInputRepository _inputRepository;
        private ICustomerOutputRepository _outputRepository;
        private ILoggingRepository _loggingRepository;
        private CustomerImportServicePure _pureService;

        public CustomerImportService(ICustomerInputRepository inputRepository, ICustomerOutputRepository outputRepository, ILoggingRepository loggingRepository)
        {
            _inputRepository = inputRepository;
            _outputRepository = outputRepository;
            _loggingRepository = loggingRepository;

            _pureService = new CustomerImportServicePure();
        }

        //returning void b/c sproc currently does not handle errors
        public void ImportCustomers()
        {
            var sapCustomers = _inputRepository.LoadCustomers();

            var customersToWrite = _pureService.DetermineInsertOrUpdate(sapCustomers);

            //note: maybe timestamps should be done by triggers, so the dev can never forget to update them
            //  (but then the timestamps of a single batch wouldn't match)
            var timeStampOfBatch = DateTime.UtcNow; //todo: shouldn't this be passed in from constructor?

            //todo: pure service mapping function should accept IEnumerable, not individual object
            //Note: another benefit of using a pure layer is better unit testing. This UT passes even if MapToCustomer throws an exception. Why? b/c the mocked repository doesn't do anything with its parameters, so the query is never evaluated
            var wcCustomersToInsert = customersToWrite.CustomersToInsert.Select(x => _pureService.MapToWCCustomer(x, timeStampOfBatch));
            var wcCustomersToUpdate = customersToWrite.CustomersToUpdate.Select(x => _pureService.MapToWCCustomer(x, timeStampOfBatch));

            _outputRepository.InsertCustomers(wcCustomersToInsert);
            _outputRepository.UpdateCustomers(wcCustomersToUpdate);

            //todo: this method should return the list of records to log
            //  * caller will call logging service
            _loggingRepository.LogRecordsAttemptedToImport(sapCustomers);

            //todo: logDaysToKeep should be a param to constructor of logging service
            var logDaysToKeep = int.Parse(ConfigurationManager.AppSettings["LogDaysToKeep"]); //todo: error handling, TryParse
            var minLogDateToKeep = _pureService.GetMinLogDateToKeep(DateTime.UtcNow, logDaysToKeep); //todo: move this logic to a new LoggingService
            _loggingRepository.DeleteLogsOlderThan(minLogDateToKeep);

            //NO TRY/CATCH (to mimic existing sproc)
        }
    }
}
