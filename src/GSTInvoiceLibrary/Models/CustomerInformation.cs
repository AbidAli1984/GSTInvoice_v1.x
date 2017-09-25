using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSTInvoiceData.Models
{
    public class CustomerInformation
    {
        [Key]
        public int Id { get; set; }

        public Guid Customerguid { get; set; }

        public string Salutaion { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        [Required(ErrorMessage = "Enter the Company Name of your Contact")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Enter the Contact Display Name")]
        [Display(Name = "Contact Display Name")]
        public string ContactDisplayName { get; set; }


        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string Website { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<ContactPerson> contactPerson { get; set; }

        public virtual CustomerOthetDetails customerOtherDetails { get; set; }

        public virtual Address address { get; set; }
    }

    public class CustomerOthetDetails
    {
        [Key, ForeignKey("customerInformation")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the Currency")]
        public string Currency { get; set; }


        [Required(ErrorMessage = "Enter the Payment term")]
        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }


        [Display(Name = "Enable Portal?")]
        public bool IsEnablePortal { get; set; }


        [Display(Name = "Portal Language")]
        public string PortalLanguage { get; set; }

        public string Facebook { get; set; }

        public string tweeter { get; set; }

        public virtual CustomerInformation customerInformation { get; set; }
    }

    public class Address
    {
        [Key, ForeignKey("customerInformation")]
        public int Id { get; set; }

        public string BillingBuilding { get; set; }

        public string BillingAddress { get; set; }

        public string BillingCity { get; set; }

        public string BillingState { get; set; }

        public string BillingPinCode { get; set; }

        public string BillingCountry { get; set; }

        public string BillingFax { get; set; }

        public string ShippingBuilding { get; set; }

        public string ShippingAddress { get; set; }

        public string ShippingCity { get; set; }

        public string ShippingState { get; set; }

        public string ShippingPinCode { get; set; }

        public string ShippingCountry { get; set; }

        public string ShippingFax { get; set; }

        public virtual CustomerInformation customerInformation { get; set; }

    }

    public class ContactPerson
    {
        [Key]
        public int Id { get; set; }

        public Guid ContactPersonguid { get; set; }


        [ForeignKey("customerInformation")]
        public int Customerid { get; set; }

        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public virtual CustomerInformation customerInformation { get; set; }

    }
}