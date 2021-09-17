using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineProdajaPica.Models
{
    public class CustomerInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public int OrderId { get; set; }
    }
}