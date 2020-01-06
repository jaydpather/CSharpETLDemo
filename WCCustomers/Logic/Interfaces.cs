using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ICustomerImportService
    {
        IEnumerable<SAPCustomer> ImportCustomers();
    }

    public interface ILoggingService
    {
        void LogCustomers(IEnumerable<SAPCustomer> sapCustomers);
    }
}
