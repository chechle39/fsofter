using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CoffeeShop.Service
{
    public class CityService :Service<City>, ICityService
    {
        //ICityRepository _cityRepository;
        //IUnitOfWork _unitOfWork;

        public CityService(IRepository<City> cityRepository, IUnitOfWork unitOfwork) : base(cityRepository, unitOfwork)
        {
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="City object"></param>
        /// <returns>int</returns>
        public int Insert(City city)
        {
            var list = base.GetAll();//Get list city.
            bool check = false;
            foreach (var item in list)
            {
                if (city.Name == item.Name)//Check name city is exist in databases.
                {
                    check = true;
                }
            }
            if (!check)//Not exist.
            {
                var rs = base.Add(city);//Insert city to entity.
                base.Save();//Save changes.
                return rs.ID;//Return new cityID inserted.
            }
            else//Exist
            {
                return -1;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="City object"></param>
        /// <returns>int</returns>
        public int Edit(City city)
        {
            base.Update(city);//Modify city to entity.
            var list = base.GetAll();//Get list city.
            int count = 0;
            foreach (var item in list)
            {
                if (city.Name == item.Name)
                {
                    count++;//Count name city the same.
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

        public IEnumerable<City> GetAllIsDelete()
        {
            return base.GetAll().Where(c => c.IsDelete == false).OrderBy(c => c.Name);
        }

        public IEnumerable<City> GetAll(string keyword)
        {
            return base.GetMulti(x => x.Name.Contains(keyword));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="cityID"></param>
        public void Delete1(int id)
        {
            var city = base.GetSingleById(id);//Get single city by id.
            city.IsDelete = true;//Modify IsDelete to 'true'.
            base.Update(city);//Modify city to entity.
        }

        /// <summary>
        /// Restore
        /// </summary>
        /// <param name="cityID"></param>
        public void Restore(int id)
        {
            var city = base.GetSingleById(id);//Get single city by id.
            city.IsDelete = false;//Modify IsDelete to 'false'.
            base.Update(city);//Modify city to entity.
        }

        public void CreateNew(City c)
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(City c)
        {
            throw new NotImplementedException();
        }

        public City GetCityByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<City> SearchByNameOrDescription(string Filter)
        {
            throw new NotImplementedException();
        }
    }
}
