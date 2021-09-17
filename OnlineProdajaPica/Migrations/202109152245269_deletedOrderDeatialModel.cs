namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedOrderDeatialModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.OrderDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        PostNumber = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
