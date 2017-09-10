using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Data.Repositories
{
    public class MaterialCategoryRepository : RepositoryBase<MaterialCategory>, IMaterialCategoryRepository
    {
        public MaterialCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}