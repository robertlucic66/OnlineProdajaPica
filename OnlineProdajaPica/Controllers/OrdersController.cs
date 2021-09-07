using Microsoft.AspNet.Identity.EntityFramework;
using OnlineProdajaPica.Models;
using OnlineProdajaPica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineProdajaPica.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;
        private IdentityDbContext _identityContext;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
            _identityContext = new IdentityDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Orders
        public ActionResult Index()
        {
            var orders = _context.Orders.ToList();
            List<OrdersViewModel> listOfOrders = new List<OrdersViewModel>();
            foreach (var item in orders)
            {
                string[] nizProducta = item.Proizvodi.Split(',');
                string[] nizKolicina = item.Kolicine.Split(',');
                List<int> nizProductaInt = new List<int>();
                List<int> nizKolicinaInt = new List<int>();
                foreach (var product in nizProducta)
                {
                    nizProductaInt.Add(Int32.Parse(product));
                }
                foreach (var kolicina in nizKolicina)
                {
                    nizKolicinaInt.Add(Int32.Parse(kolicina));
                }
                List<Product> listaProizvoda = new List<Product>();
                foreach (var product in nizProductaInt)
                {
                    var _product = _context.Products.Single(p => p.Id == product);
                    var index = nizProductaInt.IndexOf(product);
                    _product.Quantity = nizKolicinaInt[index];
                    listaProizvoda.Add(_product);
                }

                OrdersViewModel ordersVM = new OrdersViewModel(item);
                ordersVM.Username = _identityContext.Users.Single(u => u.Id == item.UserId).UserName;
                ordersVM.Products = listaProizvoda;

                listOfOrders.Add(ordersVM);
            }

            return View(listOfOrders);
        }
    }
}