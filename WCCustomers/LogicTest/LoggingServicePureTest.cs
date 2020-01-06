using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest
{
    [TestClass]
    public class LoggingServicePureTest
    {
        //todo: can't have unit tests on pure services b/c they are internal classes (unless you want to add factory methods for these)
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestMethod()
        {

        }
    }
}
