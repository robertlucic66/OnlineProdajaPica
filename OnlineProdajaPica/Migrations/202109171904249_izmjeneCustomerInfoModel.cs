namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class izmjeneCustomerInfoModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerInfoes", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerInfoes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerInfoes", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerInfoes", "City", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerInfoes", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.CustomerInfoes", "Address", c => c.String(nullable: false));
            DropColumn("dbo.CustomerInfoes", "PostNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerInfoes", "PostNumber", c => c.String());
            AlterColumn("dbo.CustomerInfoes", "Address", c => c.String());
            AlterColumn("dbo.CustomerInfoes", "Country", c => c.String());
            AlterColumn("dbo.CustomerInfoes", "City", c => c.String());
            AlterColumn("dbo.CustomerInfoes", "PhoneNumber", c => c.String());
            AlterColumn("dbo.CustomerInfoes", "Name", c => c.String());
            DropColumn("dbo.CustomerInfoes", "PostalCode");
        }
    }
}
