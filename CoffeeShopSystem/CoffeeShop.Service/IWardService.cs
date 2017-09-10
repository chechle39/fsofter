using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IWardService : IService<Ward>
    {
        IEnumerable<Ward> GetAll();

        Ward GetByID(int id);
        IEnumerable<Ward> GetByDistrictID(int districtID);
        IEnumerable<Ward> GetAllActive();

        int Insert(Ward ward);

        int Edit(Ward ward);

        void Delete1(int id);

        void Restore(int id);
    }
}
