using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GSTInvoiceData;
using GSTInvoiceData.Models;
using GSTInvoiceData.ViewModels;
using System.Data.Entity;
using System.Dynamic;

namespace Gstinvoice.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customerinfo

        GSTInvoiceDBContext customerContext = new GSTInvoiceDBContext();


        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddCustomer()
        {
            CustomerInformation customerInformation = new CustomerInformation();
            return View(customerInformation);
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerInformation customerInformation)
        {
            if (ModelState.IsValid)
            {
                GSTInvoiceData.Repository.CustomerRepository.AddorUpdateCustomer(customerInformation);
                return RedirectToAction("Contacts");
            }
            return View(customerInformation);
        }


        public ActionResult Contacts()
        {
            List<CustomerDetailViewModel> customers = GSTInvoiceData.Repository.CustomerRepository.GetCustomerForListing();
            return View(customers);
        }
    }
}