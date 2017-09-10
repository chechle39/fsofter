using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IRoleService : IService<CustomRole>
    {
        IEnumerable<CustomRole> GetAll();

        CustomRole GetByID(int id);
        IEnumerable<CustomRole> GetEmployeeRole();
    }
}
