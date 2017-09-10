using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<ApplicationGroup> GetAll();

        ApplicationGroup Add(ApplicationGroup appGroup);

        void Update(ApplicationGroup appGroup);

        ApplicationGroup Delete(int id);

        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> groups, int userId);

        IEnumerable<ApplicationGroup> GetListGroupByUserId(int userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        void Save();
    }
}
