namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanPropertyDostavljeno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Dostavljeno", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrdersViewModels", "Dostavljeno", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdersViewModels", "Dostavljeno");
            DropColumn("dbo.Orders", "Dostavljeno");
        }
    }
}
