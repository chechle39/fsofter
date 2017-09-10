using System.Collections.Generic;
using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Data.Repositories
{
    public interface IOrderRepository: IRepository<Order>
    {
        IEnumerable<Order> GetAllPagingByGroup(int group, int pageIndex, int pageSize, out int totalRow);
        IEnumerable<Order> SearchByIDandTable(string keyword);
    }
}
