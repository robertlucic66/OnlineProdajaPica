using OnlineProdajaPica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OnlineProdajaPica.Controllers
{
    public class HomeController : Controller
    {
        public List<Product> kosarica;
        public ApplicationDbContext _context;
        public HomeController()
        {
            kosarica = new List<Product>();
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            if(Session["Cart"] == null)
            {
                Session["Cart"] = kosarica;
            }
            else
            {
                kosarica = (List<Product>)Session["Cart"];
            }
            Session["CartItems"] = kosarica.Count;
            var productList = _context.Products.Include(p => p.Category).ToList();
            return View(productList);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}