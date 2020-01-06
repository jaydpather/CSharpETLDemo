using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Logic
{
    public class CustomerImportServicePure
    {
        public CustomersToInsertOrUpdate DetermineInsertOrUpdate(IEnumerable<SAPCustomer> sapCustomers)
        {
            var customersToInsert = sapCustomers.Where(x => x.CustomerId == null);
            var customersToUpdate = sapCustomers.Where(x => x.CustomerId != null);

            return new CustomersToInsertOrUpdate
            {
                CustomersToInsert = customersToInsert,
                CustomersToUpdate = customersToUpdate
            };
        }

        public IEnumerable<WCCustomer> MapToWCCustomers(IEnumerable<SAPCustomer> sapCustomer)
        {
            return sapCustomer.Select(x => MapToWCCustomer(x));
        }

        public WCCustomer MapToWCCustomer(SAPCustomer sapCustomer)
        {
            //many fields have different names, so not using auto mapper
            return new WCCustomer
            {
                Id = sapCustomer.CustomerId ?? 0,
                CustomerNumber = sapCustomer.CustomerNumber,
                Address_CountryCode = sapCustomer.CountryCode,
                Name = sapCustomer.Name,
                Address_City = sapCustomer.City,
                Address_PostalCode = sapCustomer.PostalCode,
                Address_Region = sapCustomer.Region,
                LanguageCode = sapCustomer.LanguageCode,
                VATCode = sapCustomer.VATNumber,
                IsActive = string.IsNullOrEmpty(sapCustomer.IsDeleted),
                CompanyCode = sapCustomer.CompanyCode,
                Address_CustomerType = sapCustomer.CustomerType,
                Address_StreetHouseNumber = sapCustomer.StreetHouseNumber,
                Phone = sapCustomer.Phone
            };
        }
    }

    public class CustomersToInsertOrUpdate
    {
        public IEnumerable<SAPCustomer> CustomersToInsert { get; set; }
        public IEnumerable<SAPCustomer> CustomersToUpdate { get; set; }
    }
}
