using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest
{
    [TestClass]
    public class CustomerImportServicePureTest
    {
        private CustomerImportServicePure _pureService;

        [TestInitialize]
        public void TestInitialize()
        {
            _pureService = new CustomerImportServicePure(); //in theory, we don't need to re-instantiate this before each test, b/c it's stateless
        }

        /// <summary>
        /// Note that DetermineInsertOrUpdate would normally be a private method in a Service, so you wouldn't normally test it
        /// </summary>
        [TestMethod]
        public void DetermineInsertOrUpdate()
        {
            var sapCustomers = new List<SAPCustomer>()
            {
                new SAPCustomer { CustomerId = null }, //insert
                new SAPCustomer { CustomerId = null }, //insert
                new SAPCustomer { CustomerId = 1 }, //update
            };

            var result = _pureService.DetermineInsertOrUpdate(sapCustomers);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomersToInsert);
            Assert.IsNotNull(result.CustomersToUpdate);

            Assert.AreEqual(2, result.CustomersToInsert.Count());
            Assert.AreEqual(2, result.CustomersToInsert.Count(x => x.CustomerId == null));

            Assert.AreEqual(1, result.CustomersToUpdate.Count());
            Assert.AreEqual(1, result.CustomersToUpdate.Count(x => x.CustomerId != null));
        }

        [TestMethod]
        public void MapToWCCustomers()
        {
            var sapCustomers = new List<SAPCustomer>
            {
                new SAPCustomer { Name = "abc1" },
                new SAPCustomer { Name = "abc2" },
                new SAPCustomer { Name = "abc3" },
            };

            var results = _pureService.MapToWCCustomers(sapCustomers, DateTime.Now);

            Assert.AreEqual(sapCustomers.Count, results.Count());
        }

        [TestMethod]
        public void MapToWCCustomer_NotDeleted_EmptyString()
        {
            var sapCustomer = new SAPCustomer
            {
                IsDeleted = ""
            };
            var timestampOfBatch = DateTime.UtcNow;

            var wcCustomer = _pureService.MapToWCCustomer(sapCustomer, timestampOfBatch);

            Assert.IsTrue(wcCustomer.IsActive);
            Assert.AreEqual(timestampOfBatch, wcCustomer.Timestamp);
        }

        [TestMethod]
        public void MapToWCCustomer_NotDeleted_NullString()
        {
            var sapCustomer = new SAPCustomer
            {
                IsDeleted = null
            };
            var timestampOfBatch = DateTime.UtcNow;

            var wcCustomer = _pureService.MapToWCCustomer(sapCustomer, timestampOfBatch);

            Assert.IsTrue(wcCustomer.IsActive);
            Assert.AreEqual(timestampOfBatch, wcCustomer.Timestamp);
        }

        [TestMethod]
        public void MapToWCCustomer_IsDeleted()
        {
            var sapCustomer = new SAPCustomer
            {
                IsDeleted = "a"
            };
            var timestampOfBatch = DateTime.UtcNow;

            var wcCustomer = _pureService.MapToWCCustomer(sapCustomer, timestampOfBatch);

            Assert.IsFalse(wcCustomer.IsActive);
            Assert.AreEqual(timestampOfBatch, wcCustomer.Timestamp);
        }

        [TestMethod]
        public void MapToWCCustomer_NewCustomer()
        {
            var sapCustomer = new SAPCustomer
            {
                CustomerId = null
            };
            var timestampOfBatch = DateTime.UtcNow;

            var wcCustomer = _pureService.MapToWCCustomer(sapCustomer, timestampOfBatch);

            Assert.AreEqual(0, wcCustomer.Id);
            Assert.AreEqual(timestampOfBatch, wcCustomer.Timestamp);
        }

        [TestMethod]
        public void MapToWCCustomer_ExistingCustomer()
        {
            var sapCustomer = new SAPCustomer
            {
                CustomerId = 1
            };
            var timestampOfBatch = DateTime.UtcNow;

            var wcCustomer = _pureService.MapToWCCustomer(sapCustomer, timestampOfBatch);

            Assert.AreEqual(sapCustomer.CustomerId, wcCustomer.Id);
            Assert.AreEqual(timestampOfBatch, wcCustomer.Timestamp);
        }
    }
}
