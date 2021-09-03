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
    }
}