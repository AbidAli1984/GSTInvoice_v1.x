using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.ViewModels
{
    public class InvoiceViewModel
    {
        public Guid ClientId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int PONumber { get; set; }

        public string Status { get; set; }

        public string ClientName { get; set; }

        public DateTime DueDate { get; set; }

        public int Amount { get; set; }

        public int Balance { get; set; }

        public string DrorCr { get; set; }

      
    }
}
