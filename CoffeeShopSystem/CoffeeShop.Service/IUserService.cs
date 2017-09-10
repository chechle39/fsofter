using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IUserService : IService<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> SearchByPhone(string phoneFilter);
        ApplicationUser GetByID(int id);
    }
}
