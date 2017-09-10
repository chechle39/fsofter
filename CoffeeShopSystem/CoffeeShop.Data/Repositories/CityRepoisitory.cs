using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Data.Repositories
{
    public class CityRepoisitory : RepositoryBase<City>, ICityRepository
    {
        public CityRepoisitory(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}