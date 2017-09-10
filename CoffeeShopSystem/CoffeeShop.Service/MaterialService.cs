using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoffeeShop.Service
{
    public class MaterialService : Service<Material>, IMaterialService
    {
        private IMaterialRepository materialRepository;

        public MaterialService(IRepository<Material> repoBase, IMaterialRepository repoMate, IUnitOfWork unit) : base(repoBase, unit)
        {
            this.materialRepository = repoMate;
        }

        /// <summary>
        /// Thêm 1 material sẽ tự động thêm 1 log material. bỏ cmt để xài.
        /// </summary>
        /// <param name="material">1 material cần thêm vào database</param>
        /// <returns>Material mới thêm</returns>
        public override Material Add(Material material)
        {
            var rs = materialRepository.Add(material);
            //MaterialLog mateLog = new MaterialLog()
            //{
            //    MaterialID = rs.ID,
            //    Inventory = rs.Inventory,
            //    UnitPrice = rs.UnitPrice,
            //    CreatedDate = DateTime.Now,
            //    IsDelete = false,
            //    Type = false,
            //    //EmployeeID
            //};
            //materialLogRepository.Add(mateLog);
            return rs;
        }

        /// <summary>
        /// Xóa 1 material, tự tạo material log tương tự add
        /// </summary>
        /// <param name="id">id của đối tượng cần xóa</param>
        /// <returns>TRả về đối tượng đã xóa</returns>
        public override Material Delete(int id)
        {
            var mate = GetSingleByID(id);
            mate.IsDelete = !mate.IsDelete;
            materialRepository.Update(mate);
            //MaterialLog mateLog = new MaterialLog()
            //{
            //    MaterialID = mate.ID,
            //    Inventory = mate.Inventory,
            //    UnitPrice = mate.UnitPrice,
            //    CreatedDate = DateTime.Now,
            //    IsDelete = false,
            //    Type = true,
            //    //EmployeeID
            //};
            //materialLogRepository.Add(mateLog);
            return mate;
        }

        /// <summary>
        /// Lấy ra 1 material có ID = id
        /// </summary>
        /// <param name="id">id của đối tượng cần lấy ra</param>
        /// <returns>Material</returns>
        public Material GetSingleByID(int id)
        {
            return materialRepository.GetSingleById(id);
        }

        /// <summary>
        /// Lay ra dach material dựa vào các yêu cầu.
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="status">trạng thái IsDelete</param>
        /// <param name="index">chỉ số trang hiện tại</param>
        /// <param name="size">số lượng record của trang</param>
        /// <param name="total">tổng số record lấy ra được.</param>
        /// <returns></returns>
        public IEnumerable<Material> GetSearchStatusPaging(string keyword, int status, int index, int size, out int total)
        {
            Expression<Func<Material, bool>> express;

            switch (status)
            {
                case 2:
                    express = x => x.IsDelete == false && (x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.CreatedDate.ToString().Contains(keyword) || x.UnitPrice.ToString().Contains(keyword));
                    break;

                case 3:
                    express = x => x.IsDelete == true && (x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.CreatedDate.ToString().Contains(keyword) || x.UnitPrice.ToString().Contains(keyword));
                    break;

                default:
                    express = x => (x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.CreatedDate.ToString().Contains(keyword) || x.UnitPrice.ToString().Contains(keyword));
                    break;
            }

            var query = materialRepository.GetMulti(express);
            total = query.Count();
            query = query.OrderByDescending(x => x.CreatedDate)
              .Skip((index - 1) * size)
              .Take(size).AsEnumerable();

            return query;
        }
    }
}