using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;

namespace CoffeeShop.Service
{
    /// <summary>
    /// ShopUser Service Interface
    /// </summary>
    public interface IShopUserService : IService<CustomUserRole>
    {
        CustomUserRole Create(int ShopID, int UserID, int RoleID, string Description);
        void Update(CustomUserRole group, int role, string roleDescription);
        IEnumerable<CustomUserRole> GetAll();
        CustomUserRole GetByID(int id);
        dynamic Detail(int shopUserID);
        IEnumerable<CustomUserRole> GetShopEmployee(int shopID);
        IEnumerable<CustomUserRole> GetShopEmployeeDeleted(int shopID);
        void Delete(int shopUserID, bool b = true);
        void Recover(int shopUserID);
    }
}
