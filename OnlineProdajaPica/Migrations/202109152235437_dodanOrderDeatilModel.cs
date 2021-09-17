namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanOrderDeatilModel : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.OrderDetails");
        }
    }
}
