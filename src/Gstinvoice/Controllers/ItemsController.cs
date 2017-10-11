using GSTInvoiceData;
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


        readonly GSTInvoiceDBContext _dbcontext = new GSTInvoiceDBContext();

        [HttpPost]
        public JsonResult GetUnitsByName(string name)
        {
            //Searching records from list using LINQ query  
            var cityName = (from n in _dbcontext.itemUnits.ToList()
                            where n.Name.ToLower().StartsWith(name.ToLower())
                            select new { n.Name, n.Code });
            return Json(cityName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Items()
        {
            List<ItemViewModel> items = GSTInvoiceData.Repository.ProductRepositery.GetAllItems(null);
            return View(items);
        }

        public string SearchItems(string searchKey)
        {
            List<ItemViewModel> items = GSTInvoiceData.Repository.ProductRepositery.GetAllItems(searchKey);
            string ret = CommonFunctions.RenderPartialToString("~/Views/Items/PartialViews/_ItemList.cshtml", items, ControllerContext);
            return ret;
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