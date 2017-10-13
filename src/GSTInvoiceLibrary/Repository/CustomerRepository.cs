using System;
using System.Linq;
using GSTInvoiceData.Models;
using System.Data.Entity;
using System.Collections.Generic;
using GSTInvoiceData.ViewModels;
using System.Web.Mvc;
using System.Collections;

namespace GSTInvoiceData.Repository
{
    public class CustomerRepository
    {
        static GSTInvoiceDBContext dbContext = new GSTInvoiceDBContext();

        public static CustomerInformation GetBlankCustomer()
        {
            CustomerInformation customerInformation = new CustomerInformation();
            customerInformation.customerOtherDetail = null;
            customerInformation.address = null;

            List<ContactPerson> contactPerson = new List<ContactPerson>();
            contactPerson.Add(new ContactPerson()
            {
                Salutation = "Salutation",
            });
            customerInformation.contactPersons = contactPerson;
            return customerInformation;
        }

        public static List<CustomerDetailViewModel> GetCustomerForListing(string filterExpression)
        {
            List<CustomerInformation> lstCustomer;
            if (!string.IsNullOrEmpty(filterExpression))
            {
                lstCustomer = dbContext.customerInformation
                            .Where(x => x.CompanyName.ToLower().StartsWith(filterExpression.ToLower()) ||
                                        x.ContactDisplayName.ToLower().StartsWith(filterExpression.ToLower()) ||
                                       x.address.BillingCity.ToLower().StartsWith(filterExpression.ToLower()) ||
                                        x.ContactEmail.ToLower().StartsWith(filterExpression.ToLower()) ||
                                          x.WorkPhoneNumber.ToLower().StartsWith(filterExpression.ToLower()))
                            .ToList();
            }
            else
            {
                lstCustomer = dbContext.customerInformation.ToList();
            }
            List<CustomerDetailViewModel> customers = new List<CustomerDetailViewModel>();
            foreach (CustomerInformation customer in lstCustomer)
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
            CustomerInformation cust = new CustomerInformation()
            { 
                PAN = "",

            };

            if (dbContext.customerInformation.Any(user => user.CustomerId == customerInformation.CustomerId))
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

        public static void Edit(CustomerInformation customerInformation)
        {
            AddorUpdateCustomer(customerInformation);
        }

        public static CustomerInformation GetCustomerByCustomerId(Guid Id)
        {
            CustomerInformation customerInformation = dbContext.customerInformation.SingleOrDefault(x => x.CustomerId == Id);
            return customerInformation;
        }

        public static void DeleteCustomerByCustomerId(Guid customerId)
        {

             CustomerInformation customerToDelete = dbContext.customerInformation
                                                        .Include(x => x.address)
                                                        .Include(x => x.customerOtherDetail)
                                                        .Include(x => x.contactPersons)
                                                        .FirstOrDefault(x => x.CustomerId == customerId);
            if(customerToDelete!=null)
            dbContext.customerInformation.Remove(customerToDelete);
            dbContext.SaveChanges();
        }

        public static SelectList ItemsPerPageList
        {
            get
            {
                return (new SelectList(new[] { 5, 10, 25, 50, 100 }, selectedValue: 5));
            }
        }

    }
}
