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
    public class CustomerController : Controller
    {
        // GET: Customerinfo

        GSTInvoiceDBContext userContext = new GSTInvoiceDBContext();


        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddCustomer()
        {
            CustomerInformation customerInformation = new CustomerInformation();
            customerInformation.customerOtherDetails = new CustomerOthetDetails();
            customerInformation.address = new Address();

            List<ContactPerson> contactPerson = new List<ContactPerson>();
            contactPerson.Add(new ContactPerson()
            {
                Customerid = 0,
                ContactPersonguid = Guid.NewGuid(),
                Salutation = "Dear Mr.",
                FirstName = string.Empty,
                LastName = string.Empty,
                EmailAddress = string.Empty,
                Id = 0,
                MobileNumber = string.Empty,
                WorkPhoneNumber = string.Empty
            });

            customerInformation.contactPerson = contactPerson;
            
            return View(customerInformation);
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerInformation customerInformation)
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


        public ActionResult CustomerInformation()
        {
            return View();
        }
    }
}