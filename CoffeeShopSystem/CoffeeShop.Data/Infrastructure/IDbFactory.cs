using CoffeeShop.Model.ModelEntity;
using System;

namespace CoffeeShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CoffeeSystemDbContext Init(); // 1 phương thức đễ khởi tạo dbcontext
    }
}