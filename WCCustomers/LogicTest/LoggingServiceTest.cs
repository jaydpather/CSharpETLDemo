using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Data;

namespace LogicTest
{
    [TestClass]
    public class LoggingServiceTest
    {
        ILoggingService _loggingService;
        ILoggingRepository _loggingRepository;
        DateTime _timeStampOfBatch;
        int _logDaysToKeep;

        [TestInitialize]
        public void TestInitialize()
        {
            _loggingRepository = Substitute.For<ILoggingRepository>();

            _timeStampOfBatch = DateTime.Now;
            _logDaysToKeep = 30;

            _loggingService = ServiceFactory.CreateLoggingService(_loggingRepository, _timeStampOfBatch, _logDaysToKeep);
        }

        [TestMethod]
        public void LogCustomers()
        {
            _loggingService.LogCustomers(null);

            var expectedMinLogDate = _timeStampOfBatch.AddDays(-_logDaysToKeep);

            _loggingRepository.ReceivedWithAnyArgs(1).LogRecordsAttemptedToImport(null);
            _loggingRepository.Received(1).DeleteLogsOlderThan(expectedMinLogDate);
            //note: maybe creating the Pure layer was a waste of time, b/c this UT still needs to check param value
        }
    }
}
