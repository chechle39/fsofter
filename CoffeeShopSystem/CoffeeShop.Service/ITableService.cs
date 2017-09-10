using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    public interface ITableService : IService<Table>
    {
        bool Recover(int id);
        bool Delete(int id);
        IEnumerable<Table> GetByShop(int id);
        dynamic SearchCondition(bool delete);
        dynamic SearchAdvanced(string name, int groupTableID, bool delete);
    }
}
