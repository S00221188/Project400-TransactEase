namespace Project400_TransactEase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClockInRecords",
                c => new
                    {
                        ClockInID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        ClockInTime = c.DateTime(nullable: false),
                        ClockOutTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ClockInID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductStockCount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductType = c.String(),
                        IsDiscontinued = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.SaleItems",
                c => new
                    {
                        SaleItemID = c.Int(nullable: false, identity: true),
                        SaleID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaleItemID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleID, cascadeDelete: true)
                .Index(t => t.SaleID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        SaleTime = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaleID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleItems", "SaleID", "dbo.Sales");
            DropForeignKey("dbo.Sales", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.SaleItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ClockInRecords", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Sales", new[] { "EmployeeID" });
            DropIndex("dbo.SaleItems", new[] { "ProductID" });
            DropIndex("dbo.SaleItems", new[] { "SaleID" });
            DropIndex("dbo.ClockInRecords", new[] { "EmployeeID" });
            DropTable("dbo.Sales");
            DropTable("dbo.SaleItems");
            DropTable("dbo.Products");
            DropTable("dbo.Employees");
            DropTable("dbo.ClockInRecords");
        }
    }
}
