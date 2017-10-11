using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gstinvoice.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Invoices()
        {
            return View();
        }

        public ActionResult AddInvoice()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult AddInvoice()
        //{
        //    return View();
        //}
    }
}