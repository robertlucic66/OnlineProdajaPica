﻿using OnlineProdajaPica.Models;
using OnlineProdajaPica.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Drawing;

namespace OnlineProdajaPica.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;
        public List<Product> kosarica;

        public ProductsController()
        {
            _context = new ApplicationDbContext();
            kosarica = new List<Product>();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Products
        [AllowAnonymous]
        public ActionResult Index()
        {
            Session["CurrentCategory"] = "Kategorija";

            var productList = _context.Products.Include(p=>p.Category).ToList();
            ViewBag.Categories = _context.Categories.ToList();
            if (User.IsInRole("Admin"))
                return View("AdminView", productList);
            return View("Index", productList);
        }

        public ActionResult Create()
        {
            var categories = _context.Categories.ToList();
            var viewModel = new ProductViewModel()
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            if (file != null)
            {
                product.ImageUrl = product.Id + "-" + Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("//img/products//") + product.ImageUrl);
            }

            _context.SaveChanges();            

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id < 1)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var productToEdit = _context.Products.Single(p => p.Id == id);
                var categories = _context.Categories.ToList();
                var filename = Path.GetFileName(productToEdit.ImageUrl);

                
                var viewModel = new ProductViewModel(productToEdit)
                {
                    Categories = categories,
                   
                };

                return View(viewModel);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Product updatedProduct, HttpPostedFileBase file)
        {
            try
            {
                var productToEdit = _context.Products.Single(p => p.Id == id);
                var categories = _context.Categories.ToList();
                var viewModel = new ProductViewModel(productToEdit)
                {
                    Categories = categories
                };

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                if (file != null)
                {
                    updatedProduct.ImageUrl = updatedProduct.Id + "-" + Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("//img/products//") + updatedProduct.ImageUrl);
                }
                else
                {
                    updatedProduct.ImageUrl = productToEdit.ImageUrl;
                }

                _context.Entry(productToEdit).CurrentValues.SetValues(updatedProduct);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }

        }

        public ActionResult Delete(int? id)
        {
            if (id < 1)
            {
                return Content("Neispravan ID");
            }
            try
            {
                var productToDelete = _context.Products.Single(p => p.Id == id);
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id < 1)
            {
                return Content("Neispravan ID");
            }
            try
            {
                var product = _context.Products.Include(p=>p.Category).Single(p => p.Id == id);
                return View(product);
            }
            catch{
                return Content("Greška");
            }
        }

        [Route("KategorijeRoute")]
        [AllowAnonymous]
        public ActionResult Kategorije(string id)
        {
            var productList = _context.Products.Where(p=>p.Category.Name == id).Include(p => p.Category).ToList();
            try
            {
                Session["CurrentCategory"] = _context.Categories.SingleOrDefault(c => c.Name == id).Name;
            }
            catch
            {
                Session["CurrentCategory"] = "Kategorija";
            }
            
            ViewBag.Categories = _context.Categories.ToList();
            if (productList.Count < 1)
            {
                productList = _context.Products.ToList();
            }
            return View("Index", productList);
        }
    }
}