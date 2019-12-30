using Input;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CustomerImportService
    {
        private ICustomerImportRepository _inputRepository;

        public CustomerImportService(ICustomerImportRepository inputRepository)
        {
            _inputRepository = inputRepository;
        }

        //returning void b/c sproc currently does not handle errors
        public void ImportCustomers()
        {
            //load customers from SAP repository
            //determine insert/update lists by calling pure service
            //write to output
            //write to logs
            //delete input customers loaded
            //NO TRY/CATCH (to mimic existing sproc)
            throw new NotImplementedException();
        }
    }
}
