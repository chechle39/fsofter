using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoffeeShop.Data.Repositories
{
    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Table> GetAllPagingByGroup(int groupTable, int pageIndex, int pageSize, out int totalRow)
        {
            var query = DbContext.Tables.AsEnumerable().Where(x => x.GroupTableID == groupTable && (!x.IsDelete ?? true));
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }

        /// <summary>
        /// Lấy danh sách table của shop
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List<Table></returns>
        public IEnumerable<Table> GetByShop(int id)
        {
            return GetMany(t => t.GroupTable.ShopID == id && t.IsDelete != true);
        }
    }
}