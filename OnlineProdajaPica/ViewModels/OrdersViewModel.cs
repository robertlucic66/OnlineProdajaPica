using OnlineProdajaPica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineProdajaPica.ViewModels
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public List<Product> Products { get; set; }
        public DateTime? DatumNarudzbe { get; set; }
        public bool Dostavljeno { get; set; }

        public OrdersViewModel(Order order)
        {
            Id = order.Id;
            DatumNarudzbe = order.DatumNarudzbe;
            Dostavljeno = order.Dostavljeno;
        }
    }
}