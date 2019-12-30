using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Logic
{
    internal class CustomerImportServicePure
    {
        public CustomersToInsertOrUpdate DetermineInsertOrUpdate(IEnumerable<SAPCustomer> sapCustomers)
        {
            throw new NotImplementedException();
        }
    }

    internal class CustomersToInsertOrUpdate
    {
        public IEnumerable<SAPCustomer> CustomersToInsert { get; set; }
        public IEnumerable<SAPCustomer> CustomersToUpdate { get; set; }
    }
}
