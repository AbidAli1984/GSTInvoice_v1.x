using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GSTInvoiceData;
using GSTInvoiceData.Models;
using System.Data.Entity;
using System.Dynamic;

namespace Gstinvoice.Controllers
{
    public class CustomerinfoController : Controller
    {
        // GET: Customerinfo

        GSTInvoiceDBContext userContext = new GSTInvoiceDBContext();


        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CustomerInformation()
        {
            CustomerInformation customerInformation = new CustomerInformation();
            customerInformation.customerOtherDetails = new CustomerOthetDetails();
            customerInformation.address = new Address();
            customerInformation.contactPerson = new List<ContactPerson>();
            
            return View();
        }

        [HttpPost]
        public ActionResult CustomerInformation(CustomerInformation customerInformation)
        {
            if (ModelState.IsValid)
            {
                if (!userContext.customerInformation.Any(user => user.Id == customerInformation.Id))
                {
                    customerInformation.Customerguid = Guid.NewGuid();

                    userContext.customerInformation.Add(customerInformation);
                    userContext.SaveChanges();
                    return RedirectToAction("");
                }
            }
            return View(customerInformation);
        }


        public ActionResult CustomerOtherDetails()
        {
            return View();
        }

        public ActionResult Address()
        {
            return View();
        }

        public ActionResult ContactPerson()
        {
            return View();
        }

        public ActionResult Remarks()
        {
            return View();
        }
    }
}