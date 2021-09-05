using OnlineProdajaPica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}