using OnlineProdajaPica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineProdajaPica.ViewModels;
using System.Web.Helpers;

namespace OnlineProdajaPica.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public List<Product> kosarica;

        public CartController()
        {
            _context = new ApplicationDbContext();
            kosarica = new List<Product>();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Cart        
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
            {
                kosarica = new List<Product>();
                return View(kosarica);
            }           
            kosarica = (List<Product>)Session["Cart"];
            return View(kosarica);
        }

        public ActionResult ChangeQuantity(int id, int quantity)
        {
            kosarica = (List<Product>)Session["Cart"];
            var product = kosarica.Single(p => p.Id == id);
            product.Quantity = quantity;
            Session["Cart"] = kosarica;
            return RedirectToAction("Index");
        }

        [Route("AddToCart")]
        public ActionResult AddToCart(int? id, int? quantity)
        {
            if (id == null)
                return RedirectToAction("Index", "Products");
            if (quantity == null)
            {
                TempData["Poruka"] = "Neispravan unos";
                return RedirectToAction("Index", "Products");
            }        
            if ((int)quantity < 1)
                quantity = 1;
            var productToAdd = _context.Products.Single(p => p.Id == id);
            kosarica = (List<Product>)Session["Cart"];
            string poruka;
            if (kosarica.Exists(p => p.Id == productToAdd.Id))
            {
                var productInCart = kosarica.Single(p => p.Id == productToAdd.Id);
                productInCart.Quantity += (int)quantity;
                poruka = productInCart.Name + " dodan u košaricu, količina = " + productInCart.Quantity;
                TempData["Poruka"] = poruka;
            }
            else
            {
                productToAdd.Quantity += (int)quantity;
                kosarica.Add(productToAdd);
                poruka = productToAdd.Name + " dodan u košaricu, količina = " + productToAdd.Quantity;
                TempData["Poruka"] = poruka;
            }
            Session["Cart"] = kosarica;
            return RedirectToAction("Index", "Products");
        }

        public string Proba(string ime, string prezime)
        {
            return (ime + " " + prezime);
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
                kosarica.Remove(product);
                Session["Cart"] = kosarica;
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }
        }

        public ActionResult AddCustomerInfo()
        {
            kosarica = (List<Product>)Session["Cart"];
            if (kosarica.Count < 1 || kosarica == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomerInfo(CustomerInfo customerInfo)
        {
            return RedirectToAction("OrderCheck", customerInfo);
        }

        public ActionResult OrderCheck(CustomerInfo customerInfo) 
        {
            if(customerInfo.Name == null)
            {
                return RedirectToAction("AddCustomerInfo");
            }
            kosarica = (List<Product>)Session["Cart"];

            OrderCheckViewModel orderCheckVM = new OrderCheckViewModel()
            {
                ProductList = kosarica,
                CustomerInfo = customerInfo
            };

            return View(orderCheckVM);     
        }

        public ActionResult SendOrder(CustomerInfo customerInfo)
        {
            kosarica = (List<Product>)Session["Cart"];
            if (kosarica.Count < 1)
            {
                return RedirectToAction("Index");
            }
            if(customerInfo.Name == null)
            {
                return RedirectToAction("AddCustomerInfo");
            }
            List<int> productList = new List<int>();
            List<int> quantityList = new List<int>();
            decimal totalPrice = 0;
            string proizvodi = "";
            foreach (var item in kosarica)
            {
                totalPrice += (item.Price * item.Quantity);
                productList.Add(item.Id);
                quantityList.Add(item.Quantity);
                string proizvod = "<p><b>" + item.Name + " - " + item.Quantity + "</b></p>";
                proizvodi += proizvod;
            }

            Order order = new Order()
            {
                Proizvodi = String.Join(",", productList),
                Kolicine = String.Join(",", quantityList),
                UserId = User.Identity.GetUserId(),
                DatumNarudzbe = DateTime.Now,
                Dostavljeno = false,
                UkupnaCijena = totalPrice
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            customerInfo.UserId = User.Identity.GetUserId().ToString();
            customerInfo.OrderId = order.Id;
            _context.CustomerInfos.Add(customerInfo);
            _context.SaveChanges();

            WebMail.Send(User.Identity.GetUserName().ToString(), "Narudžba ID: " + order.Id, 
                "Poštovani,<br/><br/>Vaša narudžba je zaprimljena." + proizvodi + "<b>Ukupno: " 
                + order.UkupnaCijena.ToString("0.00") + " kn</b><br/><br/>Hvala na povjerenju!",
                null,null,null,true,null,null,null,null,null,null);

            Session["Cart"] = null;

            return RedirectToAction("Index", "Cart");
        }

    }
}