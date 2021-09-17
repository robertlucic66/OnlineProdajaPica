namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanCustomerInfoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        PostNumber = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerInfoes");
        }
    }
}
