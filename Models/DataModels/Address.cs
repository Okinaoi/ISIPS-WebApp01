using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DataModels
{
    public class Address
    {
        public int AddressId { get; set; }
        public int HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
