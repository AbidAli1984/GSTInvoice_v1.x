namespace GSTInvoiceData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BillingBuilding = c.String(),
                        BillingAddress = c.String(),
                        BillingCity = c.String(),
                        BillingState = c.String(),
                        BillingPinCode = c.String(),
                        BillingCountry = c.String(),
                        BillingFax = c.String(),
                        ShippingBuilding = c.String(),
                        ShippingAddress = c.String(),
                        ShippingCity = c.String(),
                        ShippingState = c.String(),
                        ShippingPinCode = c.String(),
                        ShippingCountry = c.String(),
                        ShippingFax = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerInformations", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CustomerInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customerguid = c.Guid(nullable: false),
                        Salutaion = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyName = c.String(nullable: false),
                        ContactDisplayName = c.String(nullable: false),
                        ContactEmail = c.String(),
                        WorkPhoneNumber = c.String(),
                        MobileNumber = c.String(),
                        Website = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactPersonguid = c.Guid(nullable: false),
                        Customerid = c.Int(nullable: false),
                        Salutation = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        WorkPhoneNumber = c.String(),
                        MobileNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerInformations", t => t.Customerid, cascadeDelete: true)
                .Index(t => t.Customerid);
            
            CreateTable(
                "dbo.CustomerOthetDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Currency = c.String(nullable: false),
                        PaymentTerms = c.String(nullable: false),
                        IsEnablePortal = c.Boolean(nullable: false),
                        PortalLanguage = c.String(),
                        Facebook = c.String(),
                        tweeter = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerInformations", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Id", "dbo.CustomerInformations");
            DropForeignKey("dbo.CustomerOthetDetails", "Id", "dbo.CustomerInformations");
            DropForeignKey("dbo.ContactPersons", "Customerid", "dbo.CustomerInformations");
            DropIndex("dbo.CustomerOthetDetails", new[] { "Id" });
            DropIndex("dbo.ContactPersons", new[] { "Customerid" });
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropTable("dbo.CustomerOthetDetails");
            DropTable("dbo.ContactPersons");
            DropTable("dbo.CustomerInformations");
            DropTable("dbo.Addresses");
        }
    }
}
