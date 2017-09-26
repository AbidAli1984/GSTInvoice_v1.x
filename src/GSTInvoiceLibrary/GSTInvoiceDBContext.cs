using System.Data.Entity;
using GSTInvoiceData.Models;

namespace GSTInvoiceData
{
    public class GSTInvoiceDBContext : DbContext
    {
        public DbSet<UserInfo> userInfo { get; set; }
        public DbSet<CustomerInformation> customerInformation { get; set; }
        public DbSet<CustomerOthetDetails> customerOtherdetail { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<ContactPerson> contactPerson { get; set; }
    }
}
