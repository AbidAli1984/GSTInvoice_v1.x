using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSTInvoiceData.Models
{
    [Table("CustomerInformation")]
    public class CustomerInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        public Guid CustomerId { get; set; }

        public string Salutation { get; set; }

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

        public string Remark { get; set; }

        public virtual List<ContactPerson> contactPersons { get; set; }

        public virtual CustomerOtherDetail customerOtherDetail { get; set; }

        public virtual Address address { get; set; }

        public CustomerInformation()
        {
            customerOtherDetail = null;
            address = null;

            List<ContactPerson> contactPerson = new List<ContactPerson>();
            contactPerson.Add(new ContactPerson()
            {
                Salutation = "Salutation",
            });
            this.contactPersons = contactPerson;
        }
    }
}