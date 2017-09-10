using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Service
{

    public class WardService : Service<Ward>, IWardService
    {
        public WardService(IRepository<Ward> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        public IEnumerable<Ward> GetAllActive()
        {
            return base.GetAll().Where(x => x.IsDelete == false).ToList();
        }

        public IEnumerable<Ward> GetByDistrictID(int districtID)
        {
            return base.GetAll().Where(x => x.DistrictID == districtID).ToList();
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="Ward object"></param>
        /// <returns>int</returns>
        public int Insert(Ward ward)
        {
            var list = base.GetAll();//Get list ward.
            bool check = false;
            foreach (var item in list)
            {
                if (ward.Name == item.Name && ward.DistrictID == item.DistrictID)//Check name ward is exist in databases.
                {
                    check = true;
                }
            }
            if (!check)//Not exist.
            {
                base.Add(ward);//Insert city to entity.
                return 1;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Ward object"></param>
        /// <returns>int</returns>
        public int Edit(Ward ward)
        {
            base.Update(ward);//Modify ward to entity.
            var list = base.GetAll();//Get list ward.
            int count = 0;
            foreach (var item in list)
            {
                if (ward.Name == item.Name && ward.DistrictID == item.DistrictID)
                {
                    count++;//Count name ward the same.
                }
            }
            if (count >= 2)
            {
                return -1;//No save changes in controller.
            }
            else
            {
                return 1;//Save changes in controller.
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="wardID"></param>
        public void Delete1(int id)
        {
            var ward = base.GetSingleById(id);//Get single ward by id.
            ward.IsDelete = true;//Modify IsDelete to 'true'.
            base.Update(ward);//Modify city to entity.
        }

        /// <summary>
        /// Restore
        /// </summary>
        /// <param name="wardID"></param>
        public void Restore(int id)
        {
            var ward = base.GetSingleById(id);//Get single ward by id.
            ward.IsDelete = false;//Modify IsDelete to 'false'.
            base.Update(ward);//Modify city to entity.
        }
    }
}
