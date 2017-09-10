using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public class OrderProductRepository : RepositoryBase<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<OrderProduct> GetAllPagingByGroup(int id, int pageIndex, int pageSize, out int totalRow)
        {
            var query = DbContext.OrderProducts.AsEnumerable().Where(x => x.OrderID == id);
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }
        public IEnumerable<OrderProduct> GetListOrderProductByOrderID(int id)
        {
            return DbContext.OrderProducts.Where(od => od.OrderID == id).AsEnumerable();
        }
    }
}
