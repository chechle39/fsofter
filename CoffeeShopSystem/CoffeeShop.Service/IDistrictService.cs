using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IDistrictService : IService<District>
    {
        IEnumerable<District> GetAll();

        IEnumerable<District> GetDistrictByCityID(int id);

        IEnumerable<District> GetAllIsDelete();

        IEnumerable<District> GetAll(string keyword);

        int Insert(District district);

        int Edit(District district);

        void Delete1(int id);

        void Restore(int id);

        District GetByID(int id);

        IEnumerable<District> GetByCityID(int cityID);
    }

}
