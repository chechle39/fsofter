using Microsoft.AspNet.Identity.EntityFramework;
using CoffeeShop.Model.ModelEntity;
using System.Data.Entity;

namespace CoffeeShop.Data
{
    public class CoffeeSystemDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CoffeeSystemDbContext() : base("CoffeeSystemConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<City> Citys { set; get; }
        public DbSet<District> Districts { set; get; }
        public DbSet<GroupTable> GroupTables { set; get; }
        public DbSet<Material> Materials { set; get; }
        public DbSet<MaterialCategory> MaterialCategorys { set; get; }
        public DbSet<MaterialLog> MaterialLogs { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderProduct> OrderProducts { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategorys { set; get; }

        public DbSet<Promotion> Promotions { set; get; }

        public DbSet<Shop> Shops { set; get; }

        public DbSet<Table> Tables { set; get; }


        public DbSet<Ward> Wards { set; get; }

        // public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<CustomUserRole> CustomUserRole { set; get; }

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }


        // bài 15
        public static CoffeeSystemDbContext Create()
        {
            return new CoffeeSystemDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<CustomRole>().ToTable("Roles");
            builder.Entity<CustomUserClaim>().ToTable("UserClaims");
            builder.Entity<CustomUserLogin>().ToTable("UserLogins");
            builder.Entity<CustomUserRole>().ToTable("ShopUsers");

        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(CoffeeSystemDbContext context)
            : base(context)
        {
        }
    }

    //public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
    //CustomUserLogin, CustomUserRole, CustomUserClaim>
    //{
    //    public CustomUserStore(CoffeeSystemDbContext context)
    //        : base(context)
    //    {
    //    }
    //}
}