using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CoffeeShop.Service
{

    public class TableService : Service<Table>, ITableService
    {
        public TableService(IRepository<Table> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        /// <summary>
        /// Lấy danh sách table thuộc 1 shop cụ thể
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List<Table></returns>
        public IEnumerable<Table> GetByShop(int id)
        {
            //gọi repository
            return base.GetAll()
                .Where(t => t.GroupTable.ShopID == 1);
        }
        /// <summary>
        /// Search cơ bản
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>List<Table></returns>
        public dynamic SearchAdvanced(string name, int groupTableID, bool delete)
        {
            var key = name.ToLower();
            //Gọi SearchBase có điều kiện
            return base.GetAll()
                .Where(t => (delete == true ? t.IsDelete == true : t.IsDelete != true)
                    && (key == "" ? true : t.Name.ToLower().Contains(key))
                    && (groupTableID == 0 ? true : t.GroupTableID == groupTableID))
                .Select(t => new
                {
                    ID = t.ID,
                    Name = t.Name,
                    GroupTableName = t.GroupTable.Name,
                    GroupTableID = t.GroupTableID,
                    OrderCount = t.Order.Count,
                    Des = t.Description
                })
                .ToList();
        }

        /// <summary>
        /// Search theo option, tất cả - đã xóa - chưa xóa
        /// </summary>
        /// <param name="option">string</param>
        /// <returns>List<ID, Name, GroupTableName, GroupTableID, OrderCount, Des></returns>
        public dynamic SearchCondition(bool delete)
        {
            return base.GetAll()
                .Where(t => (delete == true ? t.IsDelete == true : t.IsDelete != true)
                    && t.GroupTable.ShopID == 1)
                .Select(t => new
                {
                    ID = t.ID,
                    Name = t.Name,
                    GroupTableName = t.GroupTable.Name,
                    GroupTableID = t.GroupTableID,
                    OrderCount = t.Order.Count,
                    Des = t.Description
                })
                .ToList();
        }

        /// <summary>
        /// Phục hồi table đã xóa
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool Recover(int id)
        {
            //Lấy thông tin table
            var toRecover = base.GetSingleById(id);
            if (toRecover == null)
            {
                //không tồn tại
                return false;
            }
            else
            {
                //cập nhật cột IsDelete
                toRecover.IsDelete = false;
                Update(toRecover);
                base.Save();
                return true;
            }
        }

        /// <summary>
        /// Xóa table
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        bool ITableService.Delete(int id)
        {
            //Lấy thông tin table
            var toDelete = base.GetSingleById(id);
            if (toDelete == null)
            {
                //không tồn tại
                return false;
            }
            else
            {
                //cập nhật cột IsDelete
                toDelete.IsDelete = true;
                Update(toDelete);
                base.Save();
                return true;
            }
        }
    }
}
