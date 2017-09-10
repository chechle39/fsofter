using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IOrderProductService
    {
        void Add(OrderProduct table);
        void Update(OrderProduct table);
        void Delete(int id);
        IEnumerable<OrderProduct> GetAll();
        OrderProduct GetByID(int id);
        void Save();
        List<OrderProduct> GetListOrderProductByOrderID(int id);
    }
}
