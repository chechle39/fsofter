using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Linq;

namespace CoffeeShop.Data.Repositories
{
    public class MaterialLogRepository : RepositoryBase<MaterialLog>, IMaterialLogRepository
    {
        public MaterialLogRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {

        }

        public void RefreshInstance(MaterialLog entity)
        {
            entity.Material = DbContext.Materials.SingleOrDefault(x => x.ID == entity.MaterialID);
            entity.ApplicationUser = DbContext.Users.SingleOrDefault(x => x.Id == entity.EmployeeID);
        }
    }
}
