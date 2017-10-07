using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GSTInvoiceData;
using GSTInvoiceData.ViewModels;
using GSTInvoiceData.Models;
using Gstinvoice;

namespace Gstinvoice.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        GSTInvoiceDBContext userContext = new GSTInvoiceDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddItem()
        {
            ItemViewModel item = new ItemViewModel();
            return View(item);
        }

        [HttpPost]
        public ActionResult AddItem(ItemViewModel item)
        {
            if (ModelState.IsValid)
            {

                GSTInvoiceData.Repository.ProductRepositery.AddProduct(item);
                return RedirectToAction("ProductDetails", "Product");

            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(item);
        }

        public ActionResult ProductDetails()
        {
            List<Items> items = GSTInvoiceData.Repository.ProductRepositery.GetAllItems();
            return View(items);
        }

        public ActionResult ProductList()
        {
            List<Items> item = GSTInvoiceData.Repository.ProductRepositery.GetAllItems();
            return View(item);
        }

        public ActionResult DeleteProduct(Guid id)
        {
            Items item = GSTInvoiceData.Repository.ProductRepositery.GetItemById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Items item)
        {
            GSTInvoiceData.Repository.ProductRepositery.Delete(item);
            return RedirectToAction("ProductInformation", "Product");
        }

        public ActionResult EditProduct(Guid id)
        {
            Items item = GSTInvoiceData.Repository.ProductRepositery.GetItemById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult EditProduct(ItemViewModel item)
        {
            GSTInvoiceData.Repository.ProductRepositery.Edit(item);
            return RedirectToAction("ProductInformation", "Product");
        }


    }

}
