using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface ICityService:IService<City>
    {
        IEnumerable<City> GetAllIsDelete();
        IEnumerable<City> GetAll(string keyword);

        int Insert(City city);

        int Edit(City city);

        void Delete1(int id);

        void Restore(int id);

        void CreateNew(City c);

        void SoftDelete(City c);

        City GetCityByID(int id);

        IEnumerable<City> SearchByNameOrDescription(string Filter);

        City GetByID(int id);
    }
}

