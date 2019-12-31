using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Logic
{
    public class CustomerImportServicePure
    {
        public CustomersToInsertOrUpdate DetermineInsertOrUpdate(IEnumerable<SAPCustomer> sapCustomers)
        {
            var customersToInsert = sapCustomers.Where(x => x.CustomerId == null);
            var customersToUpdate = sapCustomers.Where(x => x.CustomerId != null);

            return new CustomersToInsertOrUpdate
            {
                CustomersToInsert = customersToInsert,
                CustomersToUpdate = customersToUpdate
            };
        }

        public WCCustomer MapToWCCustomer(SAPCustomer sapCustomer)
        {
            throw new NotImplementedException();
        }

        public DateTime GetMinLogDateToKeep(DateTime currentDate, int daysToKeep)
        {
            return currentDate.AddDays(-daysToKeep);
        }
    }

    public class CustomersToInsertOrUpdate
    {
        public IEnumerable<SAPCustomer> CustomersToInsert { get; set; }
        public IEnumerable<SAPCustomer> CustomersToUpdate { get; set; }
    }
}
