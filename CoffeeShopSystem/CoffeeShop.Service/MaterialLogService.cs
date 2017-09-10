using System;
using System.Collections.Generic;
using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Linq;
using CoffeeShop.Model;
using System.Data.SqlClient;
using CoffeeShop.Data.Repositories;

namespace CoffeeShop.Service
{
    public class MaterialLogService : Service<MaterialLog>, IMaterialLogService
    {
        IMaterialLogRepository repo;

        public MaterialLogService(IMaterialLogRepository mateRepo, IRepository<MaterialLog> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            repo = mateRepo;
        }

        /// <summary>
        /// to flat item
        /// </summary>
        /// <param name="buf">the item to be flatten</param>
        /// <returns></returns>
        public dynamic Flat(MaterialLog buf)
        {
            if (buf != null)
            {
                var result = new { CreatedDate = buf.CreatedDate.Value.ToShortDateString(), buf.Description, buf.EmployeeID,  buf.ID, buf.Inventory, buf.IsDelete, MaterialName = (buf.Material == null ? "" : buf.Material.Name), buf.MaterialID, buf.Type, buf.UnitPrice, EmployeeName = (buf.ApplicationUser == null ? "" : buf.ApplicationUser.FullName) };
                return result;
            }

            return null;
        }

        /// <summary>
        /// to flat a bunch of items
        /// </summary>
        /// <param name="buf">item list</param>
        /// <returns></returns>
        public dynamic Flat(IEnumerable<MaterialLog> buf)
        {
            if (buf != null && buf.Count() > 0)
            {
                var result = buf
                    .Select(x => new { CreatedDate = x.CreatedDate.Value.ToShortDateString(), x.Description, x.EmployeeID, x.ID, x.Inventory, x.IsDelete, MaterialName = (x.Material == null ? "" : x.Material.Name), x.MaterialID, x.Type, x.UnitPrice, EmployeeName = (x.ApplicationUser == null ? "" : x.ApplicationUser.FullName) })
                    .ToList();
                return result;
            }

            return new List<MaterialLog>();
        }

        public void RefreshInstance(MaterialLog entity)
        {
            repo.RefreshInstance(entity);
        }

        /// <summary>
        /// to delete an item according to it's id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public new bool Delete(int id)
        {
            var item = base.GetSingleById(id);
            var result = true;

            try
            {
                if (item != null)
                {
                    item.IsDelete = true;
                    base.Update(item);
                    Save();
                }
            }
            catch (SqlException ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// to add an item to database
        /// </summary>
        /// <param name="item">the item</param>
        /// <returns></returns>
        public new MaterialLog Add(MaterialLog item)
        {
            var newItem = base.Add(item);

            try
            {
                Save();
                repo.RefreshInstance(item);
            }
            catch (SqlException ex)
            {
                newItem = null;
            }
            
            return item;
        }

        /// <summary>
        /// to update an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public new MaterialLog Update(MaterialLog item)
        {
            base.Update(item);

            try
            {
                Save();
                repo.RefreshInstance(item);
            }
            catch (SqlException ex)
            {
                item = null;
            }

            return item;
        }

        /// <summary>
        /// to search for items according to model attribute
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<MaterialLog> Search(MaterialLogSearchViewModel model)
        {
            IEnumerable<MaterialLog> result = GetAll();

            if (model.ID != 0)
            {
                result = result.Where(x => x.ID == model.ID).ToList();
            }

            if (model.FromDate != DateTime.MinValue)
            {
                result = result.Where(x => x.CreatedDate.GetValueOrDefault(DateTime.Now) >= model.FromDate).ToList();
            }

            if (model.ToDate != DateTime.MinValue)
            {
                result = result.Where(x => x.CreatedDate.GetValueOrDefault(DateTime.Now) <= model.ToDate).ToList();
            }

            if (!string.IsNullOrWhiteSpace(model.Description))
            {
                result = result.Where(x => x.Description.Contains(model.Description)).ToList();
            }

            if (model.Inventory != 0)
            {
                result = result.Where(x => x.Inventory.GetValueOrDefault(0) == model.Inventory).ToList();
            }

            if (model.UnitPrice != 0)
            {
                result = result.Where(x => x.UnitPrice.GetValueOrDefault(0) == model.UnitPrice).ToList();
            }

            if (model.Type != -1)
            {
                result = result.Where(x => x.Type.GetValueOrDefault(false) == Convert.ToBoolean(model.Type)).ToList();
            }

            if (model.IsDelete != -1)
            {
                result = result.Where(x => x.IsDelete.GetValueOrDefault(false) == Convert.ToBoolean(model.IsDelete)).ToList();
            }

            return result;
        }
    }
}
