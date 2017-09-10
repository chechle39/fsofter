using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory //kế thừa 1 inteface và 1 class.
    {
        private CoffeeSystemDbContext dbContext;
        
        public CoffeeSystemDbContext Init()
        {
            return dbContext ?? (dbContext = new CoffeeSystemDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}