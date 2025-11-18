namespace Project400_TransactEase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VatAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "VATRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "VATRate");
        }
    }
}
