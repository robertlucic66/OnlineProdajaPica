using OnlineProdajaPica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineProdajaPica.ViewModels
{
    public class OrderCheckViewModel
    {
        public List<Product> ProductList { get; set; }
        public CustomerInfo CustomerInfo {get;set;}
    }
}