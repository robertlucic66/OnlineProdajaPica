namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanModelZaNarudzbe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Proizvodi = c.String(nullable: false),
                        Kolicine = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        DatumNarudzbe = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
