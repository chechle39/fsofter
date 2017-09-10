using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop.Service
{
    

    public class RoleService : Service<CustomRole>, IRoleService
    {
        public RoleService(IRepository<CustomRole> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
           
        }        

        public IEnumerable<CustomRole> GetEmployeeRole()
        {
            return base.GetAll()/*.Where(x => x. == 3 || x.ID==5)*/;
        }
    }
}
