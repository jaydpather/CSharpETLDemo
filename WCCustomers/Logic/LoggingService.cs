using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Model;

namespace Logic
{
    internal class LoggingService : ILoggingService
    {
        ILoggingRepository _loggingRepository;
        DateTime _timeStampOfBatch;
        int _logDaysToKeep;
        LoggingServicePure _pureService;

        //todo: should timestampOfBatch and minLogDaysToKeep be params of the repository???
        public LoggingService(ILoggingRepository loggingRepository, DateTime timestampOfBatch, int logDaysToKeep)
        {
            _loggingRepository = loggingRepository;
            _timeStampOfBatch = timestampOfBatch;
            _logDaysToKeep = logDaysToKeep;

            _pureService = new LoggingServicePure();
        }

        public void LogCustomers(IEnumerable<SAPCustomer> sapCustomers)
        {
            _loggingRepository.LogRecordsAttemptedToImport(sapCustomers);

            var minLogDateToKeep = _pureService.GetMinLogDateToKeep(_timeStampOfBatch, _logDaysToKeep);
            _loggingRepository.DeleteLogsOlderThan(minLogDateToKeep);
        }
    }
}
