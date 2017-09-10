using CoffeeShop.Data.Infrastructure;
using CoffeeShop.Model.ModelEntity;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CoffeeShop.Service
{
    /// <summary>
    /// ShopUser Service
    /// </summary>
    public class ShopUserService : Service<ShopUser>, IShopUserService
    {
        public ShopUserService(IRepository<ShopUser> repo, IUnitOfWork unitOfWork) : base(repo, unitOfWork)
        {
        }

        /// <summary>
        /// Create ShopUser model
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public ShopUser Create(int shopID, int userID, int roleID, string description)
        {
            ShopUser shopUser = new ShopUser();
            shopUser.ShopID = shopID;
            shopUser.UserID = userID;
            shopUser.RoleID = roleID;
            shopUser.Description = description;
            shopUser.IsDelete = false;
            return shopUser; 
        }

        /// <summary>
        /// Get shop's employees (employee+chef)
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public IEnumerable<ShopUser> GetShopEmployee(int shopID)
        {
            //role=3:employee, role=5:chef
            return base.GetAll().Where(x => x.ShopID == shopID && (x.RoleID == 3 || x.RoleID == 5) && x.IsDelete != true);
        }

        /// <summary>
        /// Get deleted shop's employees
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public IEnumerable<ShopUser> GetShopEmployeeDeleted(int shopID)
        {

            return base.GetAll().Where(x => x.ShopID == shopID && (x.RoleID == 3 || x.RoleID == 5) && x.IsDelete == true);
        }

        /// <summary>
        /// Update ShopUser
        /// </summary>
        /// <param name="group"></param>
        /// <param name="role"></param>
        /// <param name="roleDescription"></param>
        public void Update(ShopUser group, int role, string roleDescription)
        {
            group.RoleID = role;
            group.Description = roleDescription;
            base.Update(group);
        }

        /// <summary>
        /// Delete ShopUser
        /// </summary>
        /// <param name="shopUserID"></param>
        /// <param name="temp"></param>
        public void Delete(int shopUserID, bool temp = true)
        {
            var shopUser = base.GetSingleById(shopUserID);
            shopUser.IsDelete = true;
            base.Update(shopUser);
        }

        /// <summary>
        /// Detail of ShopUser
        /// </summary>
        /// <param name="shopUserID"></param>
        /// <returns></returns>
        public dynamic Detail(int shopUserID)
        {
            var shopUser = base.GetSingleById(shopUserID);
            var dataShopUser = new
            {
                ShopID=shopUser.ShopID,
                ShopUserID = shopUser.ID,
                UserID = shopUser.UserID,
                RoleID = shopUser.RoleID,
                RoleDescription=shopUser.Description,
                Name = shopUser.User.Name,
                Username = shopUser.User.Username,
                Email = shopUser.User.Email,
                Password=shopUser.User.Password,
                DetailAddress = shopUser.User.DetailAddress,
                City = shopUser.User.Ward.District.City.ID,
                District = shopUser.User.Ward.District.ID,
                Ward = shopUser.User.Ward.ID,
                BirthDay= Convert.ToDateTime(shopUser.User.BirthDay).ToString("yyyy-MM-dd"),
                Sex=shopUser.User.Sex,
                UserDescription=shopUser.User.Description,

            };
            return dataShopUser;
        }

        /// <summary>
        /// Recover
        /// </summary>
        /// <param name="shopUserID"></param>
        public void Recover(int shopUserID)
        {
            var shopUser = base.GetSingleById(shopUserID);
            shopUser.IsDelete = false;
            base.Update(shopUser);
        }
    }
}