using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCCustomersImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var sapImportConnectionString = ConfigurationManager.AppSettings["SAPImportConnectionString"];

            var inputRepository = Data.RepositoryFactory.GetSAPImportRepository(sapImportConnectionString);
            var outputRepository = Data.RepositoryFactory.GetWCSalesRepository();
            var loggingRepository = Data.RepositoryFactory.GetLoggingRepository();

            var importService = new Logic.CustomerImportService(inputRepository, outputRepository, loggingRepository);

            importService.ImportCustomers();
        }
    }
}
