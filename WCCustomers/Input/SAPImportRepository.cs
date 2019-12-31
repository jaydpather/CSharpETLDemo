using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class SAPImportRepository : ICustomerInputRepository
    {
        private string _connectionString;

        public SAPImportRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<SAPCustomer> LoadCustomers()
        {
            

            throw new NotImplementedException();
        }

        //this method has a weird name b/c we are mimicing the behavior of the existing sproc
        //in the future, we'll only delete customers that succeeded 
        public void DeleteCustomersThatWereLoaded(IEnumerable<SAPCustomer> customers)
        {
            //THIS ONLY NEEDS TO DELETE THE CUSTOMERS WE FAILED TO SELECT
        }
    }
}
