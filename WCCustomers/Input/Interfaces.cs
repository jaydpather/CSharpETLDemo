using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Input
{
    public interface ICustomerImportRepository
    {
        IEnumerable<SAPCustomer> LoadCustomers();
        void DeleteCustomersThatWereLoaded(IEnumerable<SAPCustomer> customers);
    }
}
