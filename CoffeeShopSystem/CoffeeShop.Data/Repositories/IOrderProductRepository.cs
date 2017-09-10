using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Data.Repositories
{
    public interface IOrderProductRepository : IRepository<OrderProduct>
    {
        IEnumerable<OrderProduct> GetAllPagingByGroup(int group, int pageIndex, int pageSize, out int totalRow);
        IEnumerable<OrderProduct> GetListOrderProductByOrderID(int id);
    }
}
