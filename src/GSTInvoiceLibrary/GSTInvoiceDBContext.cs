using System.Data.Entity;
using GSTInvoiceData.Models;

namespace GSTInvoiceData
{
    public class GSTInvoiceDBContext : DbContext
    {
        public DbSet<UserInfo> userInfo { get; set; }
    }
}
