namespace ProductList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductCode = c.String(nullable: false, maxLength: 15),
                        ProductName = c.String(nullable: false, maxLength: 55),
                        Category = c.Int(nullable: false),
                        Unit = c.String(nullable: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductCode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
