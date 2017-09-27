namespace GSTInvoiceData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(),
                        Salutation = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailId = c.String(nullable: false),
                        ContactNumber = c.String(),
                        Password = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(),
                        Language = c.String(),
                        Country = c.String(),
                        UserType = c.Int(nullable: false),
                        IsEmailVerified = c.Boolean(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false),
                        RequestTokenNo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfo");
        }
    }
}
