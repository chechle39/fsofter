using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface IOrderService
    {
        void Update(Order table);
        void Delete(int id);
        IEnumerable<Order> GetAll();
        Order GetByID(int id);
        void Save();
        dynamic SearchByIdOrTable(string keyword, List<Order> lst);
        dynamic GetByStatus(string status, ref List<Order> lst);
        dynamic SearchAdvanced(string customerName, string fromDate, string toDate, int TableID, ref List<Order> lst);
    }
}
