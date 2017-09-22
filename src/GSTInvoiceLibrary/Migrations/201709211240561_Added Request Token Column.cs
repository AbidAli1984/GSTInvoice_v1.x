namespace GSTInvoiceData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequestTokenColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "RequestTokenNo", c => c.Guid(nullable: false));
            DropColumn("dbo.UserInfo", "RequestTimeOut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfo", "RequestTimeOut", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserInfo", "RequestTokenNo");
        }
    }
}
