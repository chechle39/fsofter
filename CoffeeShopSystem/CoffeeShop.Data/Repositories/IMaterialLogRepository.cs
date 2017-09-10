using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Data.Repositories
{
    public interface IMaterialLogRepository : IRepository<MaterialLog>
    {
        void RefreshInstance(MaterialLog entity);
    }
}
