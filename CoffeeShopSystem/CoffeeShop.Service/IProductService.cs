using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Service
{
    public interface IProductService : IService<Product>
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllPaging(int pageIndex, int pageSize, out int totalRow);        
        Product GetByID(int ID);
        void DeleteProduct(int id);
        void RestoreProduct(int id);

    }
}
