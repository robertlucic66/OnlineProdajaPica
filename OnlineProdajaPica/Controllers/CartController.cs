using OnlineProdajaPica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace OnlineProdajaPica.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public List<Product> kosarica;

        public CartController()
        {
            _context = new ApplicationDbContext();
            kosarica = new List<Product>();
        }
        // GET: Cart        
        public ActionResult Index()
        {
            if (Session["Cart"] != null)
            {
                kosarica = (List<Product>)Session["Cart"];
                return View(kosarica);
            }
            kosarica = new List<Product>();
            Session["Cart"] = kosarica;
            return View(kosarica);
        }

        public ActionResult AddToCart(int? id)
        {
            var productToAdd = _context.Products.Single(p => p.Id == id);
            kosarica = (List<Product>)Session["Cart"];
            if (kosarica.Exists(p => p.Id == productToAdd.Id))
            {
                var productInCart = kosarica.Single(p => p.Id == productToAdd.Id);
                productInCart.Quantity += 1;
            }
            else
            {
                productToAdd.Quantity += 1;
                kosarica.Add(productToAdd);
            }
            Session["Cart"] = kosarica;
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int? id)
        {
            if (id < 1)
            {
                return Content("Neispravan ID");
            }
            try
            {
                kosarica = (List<Product>)Session["Cart"];
                var product = kosarica.Single(p => p.Id == id);
                product.Quantity -= 1;
                if (product.Quantity < 1)
                {
                    kosarica.Remove(product);
                }
                Session["Cart"] = kosarica;
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }
        }

        public ActionResult SendOrder()
        {
            kosarica = (List<Product>)Session["Cart"];
            List<int> productList = new List<int>();
            List<int> quantityList = new List<int>();
            foreach (var item in kosarica)
            {
                productList.Add(item.Id);
                quantityList.Add(item.Quantity);
            }

            Order order = new Order()
            {
                Proizvodi = String.Join(",", productList),
                Kolicine = String.Join(",", quantityList),
                UserId = User.Identity.GetUserId(),
                DatumNarudzbe = DateTime.Now
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("Index");
        }
    }
}