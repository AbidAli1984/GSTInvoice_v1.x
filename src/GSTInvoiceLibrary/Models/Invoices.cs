using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.Models
{
    public class Invoices
    {
        [Required]
        public string ClientName { get; set; }

        public int InvoiceNumber{ get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public int PONumber { get; set; }

        public string PaymentTerms { get; set; }
    }

    
}
