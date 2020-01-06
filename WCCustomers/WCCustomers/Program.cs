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
            var inputRepository = Data.RepositoryFactory.GetSAPImportRepository("SAPImportConnectionString");
            var outputRepository = Data.RepositoryFactory.GetWCSalesRepository();
            var importService = Logic.ServiceFactory.CreateCustomerImportService(inputRepository, outputRepository);

            var logRecords = importService.ImportCustomers();

            var loggingRepository = Data.RepositoryFactory.GetLoggingRepository();
            var logDaysToKeep = int.Parse(ConfigurationManager.AppSettings["LogDaysToKeep"]); //todo: error handling, TryParse
            var loggingService = Logic.ServiceFactory.CreateLoggingService(loggingRepository, DateTime.UtcNow, logDaysToKeep);

            loggingService.LogCustomers(logRecords);
        }
    }
}