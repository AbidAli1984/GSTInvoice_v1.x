using GSTInvoiceData;
using GSTInvoiceData.Models;
using GSTInvoiceData.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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


        public ActionResult Contacts(int?page,string searchKey,string search)
        {
            List<CustomerDetailViewModel> customers = GSTInvoiceData.Repository.CustomerRepository.GetCustomerForListing(searchKey);
            //string ret = CommonFunctions.RenderPartialToString("~/Views/Customer/PartialViews/_ContactList.cshtml", customers.ToPagedList(page ?? 1, 5), ControllerContext);
            //return ret;
            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(page??1,5));
        }

        public string SearchCustomer(string searchKey)
        {
            List<CustomerDetailViewModel> customers = GSTInvoiceData.Repository.CustomerRepository.GetCustomerForListing(searchKey);
            string ret = CommonFunctions.RenderPartialToString("~/Views/Customer/PartialViews/_ContactList.cshtml", customers, ControllerContext);
            return ret;
        }

        public PartialViewResult CustomerProfileDetail(string id)
        {
            Guid customerid;
            Guid.TryParse(id, out customerid);
            CustomerInformation customer = customerContext.customerInformation.FirstOrDefault(x => x.CustomerId == customerid);
            customer.customerOtherDetail = customerContext.customerOtherdetail.FirstOrDefault(x => x.CustomerId == customerid);
            return PartialView("PartialViews/CustomerProfileDetail", customer);
        }

        public ActionResult CustomerProfileList()
        {
            List<CustomerDetailViewModel> customers = GSTInvoiceData.Repository.CustomerRepository.GetCustomerForListing(null);
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

        [HttpPost]
        public ActionResult DeleteCustomer(Guid customerId)
        {
            GSTInvoiceData.Repository.CustomerRepository.DeleteCustomerByCustomerId(customerId);
            return RedirectToAction("Contacts","Customer");
        }

       
    }
}