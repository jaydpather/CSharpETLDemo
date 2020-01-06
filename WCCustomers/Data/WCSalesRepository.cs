using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Data
{
    internal class WCSalesRepository : ICustomerOutputRepository
    {
        string _connectionStringSettingName;
        DateTime _timestampOfBatch;

        //todo: could have base class called EntityFrameworkRepository
        public WCSalesRepository(string connectionStringSettingName)
        {
            _connectionStringSettingName = connectionStringSettingName;
            _timestampOfBatch = DateTime.Now; //todo: shouldn't this be passed in?
        }

        public void InsertCustomers(IEnumerable<WCCustomer> customers)
        {
            //todo: this query needs a thread-safe way of generating the next customer ID
            var query = @"
DECLARE @customerId int
SELECT @customerId=ISNULL((MAX(Id) + 1),1000) FROM [WeConnectSales_Monkey].[dbo].[Customers]
INSERT INTO [WeConnectSales_Monkey].[dbo].[Customers]
	([Id]
	,[CustomerNumber]
	,[CompanyCode]
	,[Name]
	,[Address_City]
	,[Address_CountryCode]
	,[Phone]
	,[VATCode]
	,[LanguageCode]
	,[Timestamp]
	,[IsActive]
	,[Address_StreetHouseNumber]
	,[Address_PostalCode]
	,[Address_Region]
	,[CustomerType])
VALUES 
	(@customerId
	,@customerNumber
	,@companyCode
	,@name
	,@city 
	,@countryCode
	,@phone
	,@vatNumber
	,@languageCode
	,@timestampOfBatch
	,@isActive
	,@streetHouseNumber
	,@postalCode
	,@region
	,@customerType)";

            //todo: table-valued parameter instead of DB call inside loop
            //  * actually, in a reall app this would probably be a microservice that only upserts 1 customer at a time (while there are many copies running). So, that would also eliminate this loop.
            foreach(var curCustomer in customers)
            {
                using (var dbContext = new WCSalesDBContext(_connectionStringSettingName))
                {
                    dbContext.Database.ExecuteSqlCommand(query,
                        new SqlParameter("customerNumber", curCustomer.CustomerNumber),
                        new SqlParameter("companyCode", curCustomer.CompanyCode),
                        new SqlParameter("name", curCustomer.Name),
                        new SqlParameter("city", curCustomer.Address_City),
                        new SqlParameter("countryCode", curCustomer.Address_CountryCode),
                        new SqlParameter("phone", curCustomer.Phone),
                        new SqlParameter("vatNumber", curCustomer.VATCode),
                        new SqlParameter("languageCode", curCustomer.LanguageCode),
                        new SqlParameter("timestampOfBatch", _timestampOfBatch),
                        new SqlParameter("isActive", curCustomer.IsActive),
                        new SqlParameter("streetHouseNumber", curCustomer.Address_StreetHouseNumber),
                        new SqlParameter("postalCode", curCustomer.Address_PostalCode),
                        new SqlParameter("region", curCustomer.Address_Region),
                        new SqlParameter("customerType", curCustomer.Address_CustomerType));
                }
            }
        }

        public void UpdateCustomers(IEnumerable<WCCustomer> customers)
        {
            var query = @"
UPDATE [WeConnectSales_Monkey].[dbo].[Customers]
SET [CustomerNumber] = @customerNumber
	,[CompanyCode] = @companyCode
	,[Name] = @name
	,[Address_City] = @city
	,[Address_CountryCode] = @countryCode
	,[Phone] = @phone
	,[VATCode] = @vatNumber
	,[LanguageCode] = @languageCode
	,[Timestamp] = @timestampOfBatch
	,[IsActive] = @isActive
	,[Address_StreetHouseNumber] = @streetHouseNumber
	,[Address_PostalCode] = @postalCode
	,[Address_Region] = @region
	,[CustomerType] = @customerType
WHERE [Id] = @customerId";
            //todo: table-valued parameter instead of DB call inside loop
            //  * actually, in a reall app this would probably be a microservice that only upserts 1 customer at a time (while there are many copies running). So, that would also eliminate this loop.
            foreach (var curCustomer in customers)
            {
                using (var dbContext = new WCSalesDBContext(_connectionStringSettingName))
                {
                    dbContext.Database.ExecuteSqlCommand(query,
                        new SqlParameter("customerNumber", curCustomer.CustomerNumber),
                        new SqlParameter("companyCode", curCustomer.CompanyCode),
                        new SqlParameter("name", curCustomer.Name),
                        new SqlParameter("city", curCustomer.Address_City),
                        new SqlParameter("countryCode", curCustomer.Address_CountryCode),
                        new SqlParameter("phone", curCustomer.Phone),
                        new SqlParameter("vatNumber", curCustomer.VATCode),
                        new SqlParameter("languageCode", curCustomer.LanguageCode),
                        new SqlParameter("timestampOfBatch", _timestampOfBatch),
                        new SqlParameter("isActive", curCustomer.IsActive),
                        new SqlParameter("streetHouseNumber", curCustomer.Address_StreetHouseNumber),
                        new SqlParameter("postalCode", curCustomer.Address_PostalCode),
                        new SqlParameter("region", curCustomer.Address_Region),
                        new SqlParameter("customerType", curCustomer.Address_CustomerType),
                        new SqlParameter("customerId", curCustomer.Id));
                }
            }
        }
    }

    class WCSalesDBContext : DbContext
    {
        public WCSalesDBContext(string connectionStringName)
            :base($"name={connectionStringName}")
        {
        }
    }
}
