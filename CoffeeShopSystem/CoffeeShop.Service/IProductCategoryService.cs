using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Service
{
    public interface IProductCategoryService : IService<ProductCategory>
    {
        IEnumerable<ProductCategory> GetAll();

        ProductCategory GetByID(int id);
        void DeleteCate(int id);
        void Restore(int id);
    }
}
