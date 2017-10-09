using GSTInvoiceData.Models;
using GSTInvoiceData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Gstinvoice.Controllers
{
    public class ItemsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Items()
        {
            List<Items> items = GSTInvoiceData.Repository.ProductRepositery.GetAllItems();
            return View(items);
        }

        public ActionResult AddItem()
        {
            ItemViewModel item = new ItemViewModel();
            item.IsProduct = true;
            return View(item);
        }

        [HttpPost]
        public ActionResult AddItem(ItemViewModel item)
        {
            if (ModelState.IsValid)
            {

                GSTInvoiceData.Repository.ProductRepositery.AddOrUpdateProduct(item);
                return RedirectToAction("Items", "Items");

            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(item);
        }

        public ActionResult EditItem(Guid id)
        {
            ItemViewModel item = GSTInvoiceData.Repository.ProductRepositery.GetItemByItemId(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult EditItem(ItemViewModel item)
        {
            GSTInvoiceData.Repository.ProductRepositery.AddOrUpdateProduct(item);
            return RedirectToAction("Items", "Items");
        }

        [HttpPost]
        public ActionResult DeleteItem(Guid itemId)
        {
            GSTInvoiceData.Repository.ProductRepositery.DeleteItemsByItemId(itemId);
            return RedirectToAction("Items", "Items");
        }
    }
}