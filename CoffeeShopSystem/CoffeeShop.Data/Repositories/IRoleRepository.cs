﻿using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoffeeShop.Data.Repositories
{
    public interface IRoleRepository : IRepository<CustomRole>
    {
        IEnumerable<CustomRole> GetEmployeeRole(Expression<Func<CustomRole, bool>> expression);
    }
}