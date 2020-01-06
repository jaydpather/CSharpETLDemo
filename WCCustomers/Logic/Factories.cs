using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class ServiceFactory
    {
        public static ICustomerImportService CreateCustomerImportService(ICustomerInputRepository inputRepository, ICustomerOutputRepository outputRepository)
        {
            return new CustomerImportService(inputRepository, outputRepository);
        }

        public static ILoggingService CreateLoggingService(ILoggingRepository loggingRepository, DateTime timestampOfBatch, int logDaysToKeep)
        {
            return new LoggingService(loggingRepository, timestampOfBatch, logDaysToKeep);
        }
    }
}
