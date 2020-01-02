using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SAPCustomer
    {
        public int? CustomerId { get; set; } //WC CustomerId. null when customer does not exist.
        public string CustomerNumber { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string LanguageCode { get; set; }
        public string VATNumber { get; set; }
        public string StreetHouseNumber { get; set; }
        public string Phone { get; set; }
        public DateTime? Timestamp { get; set; }
        public string IsDeleted { get; set; }
        public string CompanyCode { get; set; }
        public string CustomerType { get; set; }
    }
}
