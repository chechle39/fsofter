using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Repositories
{
        public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
        {
            IEnumerable<ApplicationGroup> GetListGroupByUserId(int userId);
            IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
        }
}
