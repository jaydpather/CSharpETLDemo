using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class LoggingServicePure
    {
        public DateTime GetMinLogDateToKeep(DateTime currentDate, int daysToKeep)
        {
            return currentDate.AddDays(-daysToKeep);
        }
    }
}
