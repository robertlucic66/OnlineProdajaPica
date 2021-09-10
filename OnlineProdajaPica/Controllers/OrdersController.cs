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
            List<OrdersViewModel> listOfOrders = new List<OrdersViewModel>();
            
            var orders = _context.Orders.ToList();                 
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
                foreach (var idProducta in nizProductaInt)
                {
                    var index = nizProductaInt.IndexOf(idProducta);
                    var product = _context.Products.Single(p => p.Id == idProducta);
                    var _product = new Product()
                    {
                        Id = product.Id,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Name = product.Name,
                        NumberInStock = product.NumberInStock,
                        Quantity = nizKolicinaInt[index]
                    };

                    listaProizvoda.Add(_product);
                }
                
                OrdersViewModel ordersVM = new OrdersViewModel(item)
                {
                    Username = _identityContext.Users.Single(u => u.Id == item.UserId).UserName,
                    Products = listaProizvoda
                };
                
                listOfOrders.Add(ordersVM);
            }

            return View(listOfOrders.OrderByDescending(o=>o.DatumNarudzbe));
        }

        public ActionResult Details(int? id)
        {
            if (id < 1)
            {
                return Content("Nesipravan ID");
            }
            try
            {
                var order = _context.Orders.Single(o => o.Id == id);
                string[] nizProducta = order.Proizvodi.Split(',');
                string[] nizKolicina = order.Kolicine.Split(',');
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
                List<Product> productList = new List<Product>();
                foreach (var item in nizProductaInt)
                {
                    var product = _context.Products.Single(p => p.Id == item);
                    var index = nizProductaInt.IndexOf(item);
                    product.Quantity = nizKolicinaInt[index];
                    productList.Add(product);
                }

                OrdersViewModel orderVM = new OrdersViewModel(order)
                {
                    Products = productList,
                    Username = _identityContext.Users.Single(u => u.Id == order.UserId).UserName
                };
                ViewBag.UserId = order.UserId;

                return View(orderVM);
            }
            catch
            {
                return Content("Došlo je do greške");
            }
        }

        public ActionResult MarkAsDelivered(int? id)
        {
            if (id < 1)
            {
                return Content("Neispravan ID");
            }
            try
            {
                var order = _context.Orders.Single(o => o.Id == id);
                order.Dostavljeno = true;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Došlo je do greške");
            }
        }
    }
}