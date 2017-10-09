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
            CustomerInformation customerInformation = GSTInvoiceData.Repository.CustomerRepository.GetBlankCustomer();
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



        public PartialViewResult CustomerProfileDetail(string ID)
        {
            Guid customerid = new Guid();
            Guid.TryParse(ID, out customerid);
            CustomerInformation customer = customerContext.customerInformation.FirstOrDefault(x => x.CustomerId == customerid);
            customer.customerOtherDetail = customerContext.customerOtherdetail.FirstOrDefault(x => x.CustomerId == customerid);
            return PartialView("PartialViews/CustomerProfileDetail", customer);
        }

        public ActionResult CustomerProfileList()
        {
            List<CustomerDetailViewModel> customers = GSTInvoiceData.Repository.CustomerRepository.GetCustomerForListing();
            return PartialView("PartialViews/CustomerProfileList", customers);
        }

        public ActionResult EditCustomer(Guid customerId)
        {
            CustomerInformation customerInformation = GSTInvoiceData.Repository.CustomerRepository.GetCustomerByCustomerId(customerId);
            return View(customerInformation);

        }

        [HttpPost]
        public ActionResult EditCustomer(CustomerInformation customerInformation)
        {
            GSTInvoiceData.Repository.CustomerRepository.Edit(customerInformation);
            return RedirectToAction("Contacts");

        }

        public ActionResult DeleteCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteCustomer(Guid customerId)
        {
            GSTInvoiceData.Repository.CustomerRepository.DeleteCustomerByCustomerId(customerId);
            return RedirectToAction("");
        }

       
    }
}