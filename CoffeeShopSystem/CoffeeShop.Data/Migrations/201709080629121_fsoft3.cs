namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fsoft3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoles", newName: "ShopUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ShopUsers", newName: "UserRoles");
        }
    }
}
