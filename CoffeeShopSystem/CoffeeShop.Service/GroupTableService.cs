using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CoffeeShop.Service
{


    public class GroupTableService : Service<GroupTable>, IGroupTableService
    {
        public GroupTableService(IRepository<GroupTable> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        /// <summary>
        /// Lấy danh sách của shop cụ thể
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>List<GroupTable></returns>
        public IEnumerable<GroupTable> GetByShop(int id)
        {
            return base.GetAll()
                .Where(gt => gt.ShopID == id);
        }

        /// <summary>
        /// Search theo tùy chọn (đã xóa <> chưa xóa)
        /// </summary>
        /// <param name="delete">bool</param>
        /// <returns>List<ID, Name, Surcharge, TableCount, Description></returns>
        public dynamic SearchCondition(bool delete)
        {
            return base.GetAll()
                .Where(gt => (delete == true ? gt.IsDelete == true : gt.IsDelete != true)
                    && gt.ShopID == 1)
                .Select(gt => new
                {
                    ID = gt.ID,
                    Name = gt.Name,
                    Surcharge = gt.Surcharge,
                    TableCount = gt.Table.Count,
                    Description = gt.Description
                })
                .ToList();
        }

        /// <summary>
        /// Phục hồi grouptable đã xóa
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        bool IGroupTableService.Recover(int id)
        {
            var toRecover = base.GetSingleById(id);
            if (toRecover == null)
            {
                return false;
            }
            else
            {
                toRecover.IsDelete = false;
                Update(toRecover);
                Save();
                return true;
            }
        }

        /// <summary>
        /// Xóa grouptable
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        bool IGroupTableService.Delete(int id)
        {
            var toDelete = base.GetSingleById(id);
            if (toDelete == null)
            {
                return false;
            }
            else
            {
                toDelete.IsDelete = true;
                Update(toDelete);
                base.Save();
                return true;
            }
        }

        /// <summary>
        /// Search nâng cao
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="fromSurcharge">decimal</param>
        /// <param name="toSurcharge">decimal</param>
        /// <param name="delete">bool</param>
        /// <returns>List<ID, Name, Surcharge, TableCount, Description></returns>
        public dynamic SearchAdvanced(string name, decimal fromSurcharge, decimal toSurcharge, bool delete)
        {
            string key = name.ToLower();
            return base.GetAll()
                .Where(gt => (delete == true ? gt.IsDelete == true : gt.IsDelete != true)
                    && (key == "" ? true : gt.Name.ToLower().Contains(key))
                    && (fromSurcharge == 0 ? true : gt.Surcharge >= fromSurcharge)
                    && (toSurcharge == 0 ? true : gt.Surcharge <= toSurcharge)
                    && gt.ShopID == 1)
                .Select(gt => new
                {
                    ID = gt.ID,
                    Name = gt.Name,
                    Surcharge = gt.Surcharge,
                    TableCount = gt.Table.Count,
                    Description = gt.Description
                })
                .ToList();
        }
    }
}