using System;
using System.Collections.Generic;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using NSubstitute;

namespace LogicTest
{
    [TestClass]
    public class CustomerImportServiceTest
    {
        private ICustomerImportService _customerImportService;
        private ICustomerInputRepository _inputRepository;
        private ICustomerOutputRepository _outputRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _inputRepository = Substitute.For<ICustomerInputRepository>();
            _outputRepository = Substitute.For<ICustomerOutputRepository>();

            _customerImportService = ServiceFactory.CreateCustomerImportService(_inputRepository, _outputRepository);
        }

        [TestMethod]
        public void ImportCustomers_InsertAndUpdate()
        {
            var loadedCustomers = new List<SAPCustomer>
            {
                new SAPCustomer{ CustomerId = null }, //should insert b/c CustomerId is null
                new SAPCustomer{ CustomerId = 1 } //should update b/c CustomerId is not null
            };

            _inputRepository.LoadCustomers().Returns(loadedCustomers);

            var result = _customerImportService.ImportCustomers();

            _inputRepository.Received(1).LoadCustomers();
            _outputRepository.ReceivedWithAnyArgs(1).InsertCustomers(null);
            _outputRepository.ReceivedWithAnyArgs(1).UpdateCustomers(null);
            //how do we know we called Insert/UpdateCustomers w/ correct param values?
            //b/c we have another UT for CustomerImportServicePure.DetermineInsertOrUpdate()
            //it would take more mocking and knowledge of NUnit to check that the methods were called with the correct param values.

            Assert.AreEqual(loadedCustomers, result); //we should return all customers that were loaded (because that's what we want to log)
        }
    }
}
