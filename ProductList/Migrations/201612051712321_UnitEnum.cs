namespace ProductList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Unit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Unit", c => c.String(nullable: false));
        }
    }
}
