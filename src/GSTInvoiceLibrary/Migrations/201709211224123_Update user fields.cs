namespace GSTInvoiceData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateuserfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfo", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfo", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfo", "ContactNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "ContactNumber", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfo", "LastName", c => c.String());
            AlterColumn("dbo.UserInfo", "FirstName", c => c.String());
        }
    }
}
