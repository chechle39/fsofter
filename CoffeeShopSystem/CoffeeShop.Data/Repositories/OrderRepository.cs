using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
        public IEnumerable<Order> GetAllPagingByGroup(int id, int pageIndex, int pageSize, out int totalRow)
        {
            var query = DbContext.Orders.AsEnumerable().Where(x => x.ID == id);
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }


        /// <summary>
        /// Search by ID or tableName
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns> IEnumerable<Order></returns>
        public IEnumerable<Order> SearchByIDandTable(string keyword)
        {
            var query = DbContext.Orders.Where(o => o.ID.ToString().ToLower().Contains(keyword.ToLower()) || o.Table.Name.ToString().ToLower().Contains(keyword.ToLower()) || o.Users.FullName.ToLower().Contains(keyword.ToLower())).AsEnumerable();
            return query;
        }
    }
}
