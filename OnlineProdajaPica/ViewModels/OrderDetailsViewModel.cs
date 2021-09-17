using OnlineProdajaPica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineProdajaPica.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public string Username { get; set; }
        public List<Product> Products { get; set; }
    }
}