using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class CustomerImportService : ICustomerImportService
    {
        private ICustomerInputRepository _inputRepository;
        private ICustomerOutputRepository _outputRepository;
        private CustomerImportServicePure _pureService;

        public CustomerImportService(ICustomerInputRepository inputRepository, ICustomerOutputRepository outputRepository)
        {
            _inputRepository = inputRepository;
            _outputRepository = outputRepository;

            _pureService = new CustomerImportServicePure();
        }

        //returning void b/c sproc currently does not handle errors
        public IEnumerable<SAPCustomer> ImportCustomers()
        {
            var loadedSAPCustomers = _inputRepository.LoadCustomers();

            var customersToWrite = _pureService.DetermineInsertOrUpdate(loadedSAPCustomers);

            //Note: another benefit of using a pure layer is better unit testing. This UT passes even if MapToCustomer throws NotImplementedException. Why? b/c the mocked repository doesn't do anything with its parameters, so the query is never evaluated
            var wcCustomersToInsert = _pureService.MapToWCCustomers(customersToWrite.CustomersToInsert);
            var wcCustomersToUpdate = _pureService.MapToWCCustomers(customersToWrite.CustomersToUpdate);

            //todo: procedural style
            //  * the input to UpdateCustomers is not the return value of InsertCustomers (nor should it be, b/c that wouldn't make sense)
            //  * db layer could have an upsert function, but then we'd be putting logic into the data layer. maybe that's actually good, b/c it's logic about which field is the primary key, and if it has a null value
            _outputRepository.InsertCustomers(wcCustomersToInsert);
            _outputRepository.UpdateCustomers(wcCustomersToUpdate);

            return loadedSAPCustomers;
            //NO TRY/CATCH (to mimic existing sproc)
        }
    }
}