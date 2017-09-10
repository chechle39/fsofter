namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fsoft : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        CityID = c.Int(nullable: false),
                        Description = c.String(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Citys", t => t.CityID, cascadeDelete: true)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DistrictID = c.Int(nullable: false),
                        Name = c.String(maxLength: 255),
                        Description = c.String(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Districts", t => t.DistrictID, cascadeDelete: true)
                .Index(t => t.DistrictID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        BirthDay = c.DateTime(),
                        WardID = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wards", t => t.WardID)
                .Index(t => t.WardID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        WardID = c.Int(),
                        DetailAddress = c.String(maxLength: 255),
                        Description = c.String(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Wards", t => t.WardID)
                .Index(t => t.WardID);
            
            CreateTable(
                "dbo.GroupTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(storeType: "ntext"),
                        Surcharge = c.Decimal(storeType: "money"),
                        IsDelete = c.Boolean(),
                        ShopID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Shops", t => t.ShopID)
                .Index(t => t.ShopID);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        GroupTableID = c.Int(nullable: false),
                        Description = c.String(storeType: "ntext"),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GroupTables", t => t.GroupTableID, cascadeDelete: true)
                .Index(t => t.GroupTableID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TableID = c.Int(nullable: false),
                        PromotionID = c.Int(),
                        SetDate = c.DateTime(),
                        TotalMoney = c.Decimal(storeType: "money"),
                        Status = c.Boolean(),
                        isDelete = c.Boolean(),
                        ShopID = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Promotions", t => t.PromotionID)
                .ForeignKey("dbo.Shops", t => t.ShopID, cascadeDelete: true)
                .ForeignKey("dbo.Tables", t => t.TableID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.TableID)
                .Index(t => t.PromotionID)
                .Index(t => t.ShopID)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(storeType: "money"),
                        Money = c.Decimal(storeType: "money"),
                        isDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductCategoryID = c.Int(nullable: false),
                        ShopID = c.Int(),
                        Name = c.String(nullable: false, maxLength: 255),
                        UnitPrice = c.Decimal(storeType: "money"),
                        Desciption = c.String(storeType: "ntext"),
                        StatDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Discount = c.Int(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductCategory", t => t.ProductCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.ShopID)
                .Index(t => t.ProductCategoryID)
                .Index(t => t.ShopID);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(storeType: "ntext"),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShopID = c.Int(),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        BasePurchase = c.Decimal(precision: 18, scale: 2),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Shops", t => t.ShopID)
                .Index(t => t.ShopID);
            
            CreateTable(
                "dbo.MaterialCategorys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CategoryID = c.Int(),
                        UnitPrice = c.Decimal(storeType: "money"),
                        Inventory = c.Int(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialCategorys", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.MaterialLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaterialID = c.Int(),
                        EmployeeID = c.Int(),
                        Inventory = c.Int(),
                        UnitPrice = c.Decimal(storeType: "money"),
                        Type = c.Boolean(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.EmployeeID)
                .ForeignKey("dbo.Materials", t => t.MaterialID)
                .Index(t => t.MaterialID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MaterialLogs", "MaterialID", "dbo.Materials");
            DropForeignKey("dbo.MaterialLogs", "EmployeeID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Materials", "CategoryID", "dbo.MaterialCategorys");
            DropForeignKey("dbo.Shops", "WardID", "dbo.Wards");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "TableID", "dbo.Tables");
            DropForeignKey("dbo.Orders", "ShopID", "dbo.Shops");
            DropForeignKey("dbo.Promotions", "ShopID", "dbo.Shops");
            DropForeignKey("dbo.Orders", "PromotionID", "dbo.Promotions");
            DropForeignKey("dbo.Products", "ShopID", "dbo.Shops");
            DropForeignKey("dbo.Products", "ProductCategoryID", "dbo.ProductCategory");
            DropForeignKey("dbo.OrderProducts", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Tables", "GroupTableID", "dbo.GroupTables");
            DropForeignKey("dbo.GroupTables", "ShopID", "dbo.Shops");
            DropForeignKey("dbo.Wards", "DistrictID", "dbo.Districts");
            DropForeignKey("dbo.AspNetUsers", "WardID", "dbo.Wards");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Districts", "CityID", "dbo.Citys");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MaterialLogs", new[] { "EmployeeID" });
            DropIndex("dbo.MaterialLogs", new[] { "MaterialID" });
            DropIndex("dbo.Materials", new[] { "CategoryID" });
            DropIndex("dbo.Promotions", new[] { "ShopID" });
            DropIndex("dbo.Products", new[] { "ShopID" });
            DropIndex("dbo.Products", new[] { "ProductCategoryID" });
            DropIndex("dbo.OrderProducts", new[] { "ProductID" });
            DropIndex("dbo.OrderProducts", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "ShopID" });
            DropIndex("dbo.Orders", new[] { "PromotionID" });
            DropIndex("dbo.Orders", new[] { "TableID" });
            DropIndex("dbo.Tables", new[] { "GroupTableID" });
            DropIndex("dbo.GroupTables", new[] { "ShopID" });
            DropIndex("dbo.Shops", new[] { "WardID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "WardID" });
            DropIndex("dbo.Wards", new[] { "DistrictID" });
            DropIndex("dbo.Districts", new[] { "CityID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MaterialLogs");
            DropTable("dbo.Materials");
            DropTable("dbo.MaterialCategorys");
            DropTable("dbo.Promotions");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Products");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Orders");
            DropTable("dbo.Tables");
            DropTable("dbo.GroupTables");
            DropTable("dbo.Shops");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Wards");
            DropTable("dbo.Districts");
            DropTable("dbo.Citys");
        }
    }
}
