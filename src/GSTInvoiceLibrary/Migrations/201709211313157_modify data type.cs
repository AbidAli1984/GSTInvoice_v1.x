namespace GSTInvoiceData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifydatatype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "RequestTokenNo", c => c.String());
        }

        public override void Down()
        {
        }
    }
}
