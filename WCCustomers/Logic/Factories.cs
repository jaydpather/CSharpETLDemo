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
        public static ICustomerImportService CreateCustomerImportService(ICustomerInputRepository inputRepository, ICustomerOutputRepository outputRepository, ILoggingRepository loggingRepository)
        {
            return new CustomerImportService(inputRepository, outputRepository, loggingRepository);
        }
    }
}
