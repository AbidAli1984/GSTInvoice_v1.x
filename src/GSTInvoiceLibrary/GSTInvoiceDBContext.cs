using System.Data.Entity;
using GSTInvoiceData.Models;

namespace GSTInvoiceData
{
    public class GSTInvoiceDBContext : DbContext
    {
        public DbSet<UserInfo> userInfo { get; set; }
        public DbSet<CustomerInformation> customerInformation { get; set; }
        public DbSet<CustomerOtherDetail> customerOtherdetail { get; set; }
        public DbSet<Address> address { get; set; }
        public DbSet<ContactPerson> contactPerson { get; set; }
        public DbSet<Country> country { get; set; }

        public DbSet<State> state { get; set; }

        public DbSet<City> city { get; set; }

        public DbSet<Items> items { get; set; }

    }
}
