using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSTInvoiceData.Models
{
    [Table("CustomerOtherDetail")]
    public class CustomerOtherDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key, ForeignKey("customerInformation")]
        public Guid CustomerId { get; set; }

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
}
