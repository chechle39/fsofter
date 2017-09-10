using CoffeeShop.Model;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IMaterialLogService : IService<MaterialLog>
    {
        dynamic Flat(MaterialLog buf);
        dynamic Flat(IEnumerable<MaterialLog> buf);
        new bool Delete(int id);
        new MaterialLog Add(MaterialLog item);
        new MaterialLog Update(MaterialLog item);
        IEnumerable<MaterialLog> Search(MaterialLogSearchViewModel model);
        void RefreshInstance(MaterialLog entity);
    }
}
