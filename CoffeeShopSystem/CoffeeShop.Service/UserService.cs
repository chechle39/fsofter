using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{


    public class UserService : Service<ApplicationUser>, IUserService
    {
        public UserService(IRepository<ApplicationUser> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        public IEnumerable<ApplicationUser> SearchByPhone(string phoneFilter)
        {
            if (phoneFilter == "")
            {
                return base.GetAll();
            }
            return base.GetMulti(u => u.FullName == phoneFilter);
        }
    }
}
