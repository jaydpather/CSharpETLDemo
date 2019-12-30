using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input
{
    public static class CustomerImportFactory
    {
        public static ICustomerImportRepository GetSAPImportRepository()
        {
            return new SAPImportRepository();
        }
    }
}
