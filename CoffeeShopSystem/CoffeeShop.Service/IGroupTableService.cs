using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Service
{
    public interface IGroupTableService : IService<GroupTable>
    {
        bool Recover(int id);
        bool Delete(int id);
        IEnumerable<GroupTable> GetByShop(int id);
        dynamic SearchCondition(bool delete);
        dynamic SearchAdvanced(string name, decimal formSurcharge, decimal toSurcharge, bool delete);
    }
}
