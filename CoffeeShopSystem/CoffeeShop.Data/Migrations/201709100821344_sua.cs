namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sua : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Description", c => c.String(maxLength: 250));
            DropColumn("dbo.ShopUsers", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopUsers", "Description", c => c.String(maxLength: 250));
            DropColumn("dbo.Roles", "Description");
        }
    }
}
