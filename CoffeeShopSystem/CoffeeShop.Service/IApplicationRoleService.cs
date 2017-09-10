using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Service
{
    public interface IApplicationRoleService
    {
        CustomRole GetDetail(int id);

        IEnumerable<CustomRole> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<CustomRole> GetAll();

        CustomRole Add(CustomRole appRole);

        void Update(CustomRole AppRole);

        void Delete(int id);

        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId);

        //Get list role by group id
        IEnumerable<CustomRole> GetListRoleByGroupId(int groupId);

        void Save();
    }
}
