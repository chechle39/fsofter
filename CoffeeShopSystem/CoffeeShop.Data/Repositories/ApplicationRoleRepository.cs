using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Data.Repositories
{

    public interface IApplicationRoleRepository : IRepository<CustomRole>
    {
        IEnumerable<CustomRole> GetListRoleByGroupId(int groupId);
    }
    public class ApplicationRoleRepository : RepositoryBase<CustomRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public IEnumerable<CustomRole> GetListRoleByGroupId(int groupId)
        {
            var query = from g in DbContext.Roles
                        join ug in DbContext.ApplicationRoleGroups
                        on g.Id equals ug.RoleId
                        where ug.GroupId == groupId
                        select g;
            return query;
        }
    }
}
