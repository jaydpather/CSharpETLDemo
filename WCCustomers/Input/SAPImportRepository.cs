using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    //todo: maybe each repository should be its own project
    //  * currently, if we are loading from different sources like EF, Oracle, txt file, etc, then we will reference the libaries for all those data sources in 1 project
    //  * seems cleaner to have projects that only reference the libraries they need
    internal class SAPImportRepository : ICustomerInputRepository
    {
        //SAPImportDBContext _dbContext;
        string _connectionStringSettingName;

        public SAPImportRepository(string connectionStringSettingName)
        {
            _connectionStringSettingName = connectionStringSettingName;

            //this is a workaround necessary for an EF bug:
            var ensureDLLIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public IEnumerable<SAPCustomer> LoadCustomers()
        {
            var query = @"SELECT LTRIM(RTRIM(CAST(CB.[CustomerNumber] AS NVARCHAR(10)))) AS [CustomerNumber]
      ,CB.[CountryCode]
      ,CB.[Name]
      ,CB.[City]
      ,CB.[PostalCode]
      ,CB.[Region]
      ,CB.[LanguageCode]
      ,CB.[VATNumber]
      ,CB.[StreetHouseNumber]
      ,CB.[Phone]
      ,CB.[Timestamp]
	  ,CASE WHEN LEN(CB.[CustomerType])=0 OR CB.[CustomerType] IS NULL THEN '00' ELSE CB.[CustomerType] END as CustomerType
      ,CASE LEN(CB.[IsDeleted]) WHEN 0 THEN NULL ELSE CB.[IsDeleted] END as IsDeleted
	  ,CC.CompanyCode
	  ,C.[Id] As CustomerId  
	FROM [dbo].[CustomerBasic] CB
	INNER JOIN [dbo].[CustomerCompany] CC ON CC.CustomerNumber=CB.CustomerNumber COLLATE DATABASE_DEFAULT
	LEFT OUTER JOIN [WeConnectSales_TEST].[dbo].[Customers] C ON (C.CustomerNumber=CB.CustomerNumber COLLATE DATABASE_DEFAULT AND C.CompanyCode=CC.CompanyCode COLLATE DATABASE_DEFAULT)
	WHERE CC.CompanyCode IN ('W031','TH31','INLC','TH90','TH47','CK07','CK47','PB31','3906','PVHE')";

            //Todo: folder name of this project does not match project name
            using (var dbContext = new SAPImportDBContext(_connectionStringSettingName))
            {
                return dbContext.Database.SqlQuery<SAPCustomer>(query).ToList();
            }
        }

        //this method has a weird name b/c we are mimicing the behavior of the existing sproc
        //in the future, we'll only delete customers that succeeded 
        public void DeleteCustomersThatWereLoaded(IEnumerable<SAPCustomer> customers)
        {
            //THIS ONLY NEEDS TO DELETE THE CUSTOMERS WE FAILED TO SELECT
        }
    }

    //todo: different file? new folder?
    class SAPImportDBContext : DbContext
    {
        public SAPImportDBContext(string connectionStringSettingName)
            :base($"name={connectionStringSettingName}")
        {
        }
    }
}
