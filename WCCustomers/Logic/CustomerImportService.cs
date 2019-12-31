using Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
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

            var wcCustomersToInsert = customersToWrite.CustomersToInsert.Select(x => _pureService.MapToWCCustomer(x));
            var wcCustomersToUpdate = customersToWrite.CustomersToUpdate.Select(x => _pureService.MapToWCCustomer(x));

            _outputRepository.InsertCustomers(wcCustomersToInsert);
            _outputRepository.UpdateCustomers(wcCustomersToUpdate);

            _loggingRepository.LogRecordsAttemptedToImport(sapCustomers);

            var logDaysToKeep = ConfigurationManager.AppSettings["LogDaysToKeep"];
            var minLogDateToKeep = _pureService.GetMinLogDateToKeep(DateTime.UtcNow, 30);
            _loggingRepository.DeleteLogsOlderThan(minLogDateToKeep);

            //NO TRY/CATCH (to mimic existing sproc)
            throw new NotImplementedException();
        }
    }
}
