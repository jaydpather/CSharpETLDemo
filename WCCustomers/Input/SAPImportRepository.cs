using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input
{
    internal class SAPImportRepository : ICustomerImportRepository
    {
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
