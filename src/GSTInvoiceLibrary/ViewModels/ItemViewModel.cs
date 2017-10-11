using System;
using System.ComponentModel.DataAnnotations;

namespace GSTInvoiceData.ViewModels
{
    public class ItemViewModel
    {
        public Guid ItemId { get; set; }

        public bool IsProduct { get; set; }

        [Required(ErrorMessage = "Please Enter name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public string Tax { get; set; }

        public string HSNorSAC { get; set; }

        public int UnitPrice { get; set; }

        public string Currency { get; set; }

        public string Quantity { get; set; }

        public string ProductType { get; set; }

        public ItemViewModel()
        {
            ProductType = IsProduct ? "Product" : "Service";
        }
    }
}
