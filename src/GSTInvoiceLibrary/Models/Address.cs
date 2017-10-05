using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.Models
{
    [Table("Address")]
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key, ForeignKey("customerInformation")]
        public Guid CustomerId { get; set; }

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
}
