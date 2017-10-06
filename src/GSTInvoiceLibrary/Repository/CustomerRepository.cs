using System;
using System.Linq;
using GSTInvoiceData.Models;
using System.Data.Entity;
using System.Collections.Generic;
using GSTInvoiceData.ViewModels;

namespace GSTInvoiceData.Repository
{
    public class CustomerRepository
    {
        static GSTInvoiceDBContext dbContext = new GSTInvoiceDBContext();

        public static List<CustomerDetailViewModel> GetCustomerForListing()
        {
            List<CustomerDetailViewModel> customers = new List<CustomerDetailViewModel>();
            foreach (CustomerInformation customer in dbContext.customerInformation.OrderBy(x => x.Id).ToList())
            {
                CustomerDetailViewModel customerDetail = new CustomerDetailViewModel();
                customerDetail.customerId = customer.CustomerId;
                customerDetail.CompanyName = customer.CompanyName;
                customerDetail.Email = customer.ContactEmail;
                customerDetail.ContactName = customer.ContactDisplayName;
                customerDetail.WorkPhone = customer.WorkPhoneNumber;

                Address address = customer.address;
                customerDetail.City = address.BillingCity ?? address.ShippingCity;
                customerDetail.Receivables = 0.00M;

                customers.Add(customerDetail);
            }
            return customers;
        }

        public static void AddorUpdateCustomer(CustomerInformation customerInformation)
        {
            if (dbContext.customerInformation.Any(user => user.Id == customerInformation.Id))
            {
                dbContext.Entry(customerInformation).State = EntityState.Modified;
            }
            else
            {
                customerInformation.CustomerId = Guid.NewGuid();


                customerInformation.contactPersons.RemoveAll(contactPerson => string.IsNullOrEmpty(contactPerson.FirstName) && string.IsNullOrEmpty(contactPerson.LastName) &&
                        string.IsNullOrEmpty(contactPerson.EmailAddress) && string.IsNullOrEmpty(contactPerson.WorkPhoneNumber) &&
                        string.IsNullOrEmpty(contactPerson.MobileNumber));
                foreach (ContactPerson contactPerson in customerInformation.contactPersons)
                {
                    contactPerson.ContactPersonId = Guid.NewGuid();
                }
                if (customerInformation.contactPersons.Count <= 0)
                {
                    ContactPerson contactPerson = new ContactPerson();
                    contactPerson.ContactPersonId = Guid.NewGuid();
                    customerInformation.contactPersons.Add(contactPerson);
                }
                dbContext.customerInformation.Add(customerInformation);
            }
            dbContext.SaveChanges();
        }
    }
}
