using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTInvoiceData.ViewModels
{
    public class CustomerDetailViewModel
    {
        public Guid customerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public decimal Receivables { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public int PageSize { get; set; }
    }
}
