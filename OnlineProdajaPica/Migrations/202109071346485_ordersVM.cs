namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordersVM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdersViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        DatumNarudzbe = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "OrdersViewModel_Id", c => c.Int());
            CreateIndex("dbo.Products", "OrdersViewModel_Id");
            AddForeignKey("dbo.Products", "OrdersViewModel_Id", "dbo.OrdersViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "OrdersViewModel_Id", "dbo.OrdersViewModels");
            DropIndex("dbo.Products", new[] { "OrdersViewModel_Id" });
            DropColumn("dbo.Products", "OrdersViewModel_Id");
            DropTable("dbo.OrdersViewModels");
        }
    }
}
