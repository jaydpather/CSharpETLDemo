using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    //todo: different factories for input, output, and logging?
    public static class RepositoryFactory
    {
        public static ICustomerInputRepository GetSAPImportRepository(string connectionStringSettingName)
        {
            return new SAPImportRepository(connectionStringSettingName);
        }

        public static ICustomerOutputRepository GetWCSalesRepository()
        {
            return new WCSalesRepository();
        }

        public static ILoggingRepository GetLoggingRepository()
        {
            return new LoggingRepository();
        }
    }
}
