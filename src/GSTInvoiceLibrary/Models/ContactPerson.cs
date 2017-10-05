using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.Models
{
    [Table("ContactPerson")]
    public class ContactPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("customerInformation")]
        public Guid CustomerId { get; set; }

        public Guid ContactPersonId { get; set; }

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
