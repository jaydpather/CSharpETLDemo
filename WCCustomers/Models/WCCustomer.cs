using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WCCustomer
    {
        public int Id { get; set; }
        public string CustomerNumber { get; set; }
        public string CompanyCode { get; set; }
        public string Name { get; set; }
        public string Address_City { get; set; }
        public string Address_CountryCode { get; set; }
        public string Phone { get; set; }
        public string VATCode { get; set; }
        public string LanguageCode { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool IsActive { get; set; }
        public string Address_StreetHouseNumber { get; set; }
        public string Address_PostalCode { get; set; }
        public string Address_Region { get; set; }
        public string Address_CustomerType { get; set; }
    }
}
