using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CoffeeShop.Service
{
    
    public class DistrictService : Service<District>, IDistrictService
    {

        public DistrictService(IRepository<District> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="District object"></param>
        /// <returns>int</returns>
        public int Insert(District district)
        {
            var list = base.GetAll();//Get list district.
            bool check = false;
            foreach (var item in list)
            {
                if (district.Name == item.Name && district.CityID == item.CityID)//Check name district is exist in databases.
                {
                    check = true;
                }
            }
            if (!check)//Not exist.
            {
                base.Add(district);//Insert city to entity.
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
        /// <param name="District object"></param>
        /// <returns>int</returns>
        public int Edit(District district)
        {
            base.Update(district);//Modify district to entity.
            var list = base.GetAll();//Get list district.
            int count = 0;
            foreach (var item in list)
            {
                if (district.Name == item.Name && district.CityID == item.CityID)
                {
                    count++;//Count name district the same.
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

        public IEnumerable<District> GetDistrictByCityID(int id)
        {

            return base.GetAll().Where(d => d.CityID == id && d.IsDelete == false);
        }

        public IEnumerable<District> GetAllIsDelete()
        {

            return base.GetAll().Where(c => c.IsDelete == false).OrderBy(c => c.Name);
        }

        public IEnumerable<District> GetAll(string keyword)
        {

            return base.GetMulti(x => x.Name.Contains(keyword));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="districtID"></param>
        public void Delete1(int id)
        {
            var district = base.GetSingleById(id);//Get single district by id.
            district.IsDelete = true;//Modify IsDelete to 'true'.
            base.Update(district);//Modify city to entity.
        }

        /// <summary>
        /// Restore
        /// </summary>
        /// <param name="districtID"></param>
        public void Restore(int id)
        {
            var district = base.GetSingleById(id);//Get single district by id.
            district.IsDelete = false;//Modify IsDelete to 'false'.
            base.Update(district);//Modify city to entity.
        }

        public IEnumerable<District> GetByCityID(int cityID)
        {
             return base.GetAll().Where(x => x.CityID == cityID && x.IsDelete == false);
        }
		
    }
}
