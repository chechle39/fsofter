using System;
using System.Collections.Generic;
using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Linq.Expressions;

namespace CoffeeShop.Data.Repositories
{
    public class RoleRepository : RepositoryBase<CustomRole>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<CustomRole> GetEmployeeRole(Expression<Func<CustomRole, bool>> expression)
        {
            return GetMany(expression);
        }
    }
}