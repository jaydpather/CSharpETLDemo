using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Data
{
    public interface ICustomerInputRepository
    {
        IEnumerable<SAPCustomer> LoadCustomers();
        void DeleteCustomersThatWereLoaded(IEnumerable<SAPCustomer> customers);
    }

    public interface ICustomerOutputRepository
    {
        void InsertCustomers(IEnumerable<WCCustomer> customers);
        void UpdateCustomers(IEnumerable<WCCustomer> customers);
    }

    public interface ILoggingRepository
    {
        void LogRecordsAttemptedToImport(IEnumerable<SAPCustomer> sapCustomers);
        void DeleteLogsOlderThan(DateTime minDateExclusive);
    }
}
