using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoffeeShop.Data.Repositories
{
    public class ShopUserRepository : RepositoryBase<CustomUserRole>, IShopUserRepository
    {
        public ShopUserRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<CustomUserRole> GetShopEmployee(Expression<Func<CustomUserRole, bool>> expression)
        {
            return GetMany(expression);
        }
    }
}
