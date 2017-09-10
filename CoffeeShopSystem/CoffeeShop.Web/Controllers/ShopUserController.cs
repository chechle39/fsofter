using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System.Linq;
using System.Web.Mvc;


namespace CoffeeShop.Web.Controllers
{
    /// <summary>
    /// ShopUser Controller
    /// </summary>
    public class ShopUserController : Controller
    {
        //initialize service object
        IShopUserService shopUserService;
        ICityService cityService;
        IDistrictService districtService;
        IWardService wardService;
        IRoleService roleService;
        IUserService userService;

        public ShopUserController(IShopUserService ShopUserService, ICityService CityService, IDistrictService DistrictService, IWardService WardService, IRoleService RoleService, IUserService UserService)
        {
            this.shopUserService = ShopUserService;
            this.cityService = CityService;
            this.districtService = DistrictService;
            this.wardService = WardService;
            this.roleService = RoleService;
            this.userService = UserService;
        }

        /// <summary>
        /// GET:Detail  ShopUser
        /// </summary>
        /// <param name="shopUserID"></param>
        /// <returns>ShopUser</returns>
        public JsonResult Detail(int shopUserID)
        {
            var shopUser = shopUserService.Detail(shopUserID);
            return Json(shopUser, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET: ShopUser
        /// </summary>
        /// <returns>List<ShopUser></ShopUser></returns>
        public ActionResult Index()
        {
            //(int)shopUserService.GetByID(3):số 3 sẽ được thay bằng userID đang đăng nhập khi bổ sung Login
            var employee = shopUserService.GetShopEmployee((int)shopUserService.GetByID(3).ShopID).ToList();
            ViewBag.CityList = cityService.GetAll().ToList();
            //Get role:Employee and chef
            ViewBag.RoleList = roleService.GetEmployeeRole().ToList();
            return View(employee);
        }
        /// <summary>
        /// GET: List of district by cityID
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns>List<District></returns>
        public JsonResult DistrictListByCityID(int cityID)
        {
            var districtList = districtService.GetByCityID(cityID)
                .Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET: List of ward by districtID
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public JsonResult WardListByDistrictID(int districtID)
        {
            var districtList = wardService.GetByDistrictID(districtID)
                .Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// POST-Add new user-shopuser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="roleDescription"></param>
        /// <returns>ShopUser-Added</returns>
        [HttpPost]
        public JsonResult Add(User user, int role, string roleDescription)
        {
            userService.Add(user);
            //userService.Save();
            ShopUser shopUser = new ShopUser();
            //(int)shopUserService.GetByID(3):số 3 sẽ được thay bằng userID đang đăng nhập khi bổ sung Login
            shopUser = shopUserService.Create((int)shopUserService.GetByID(3).ShopID, user.ID, role, roleDescription);
            shopUserService.Add(shopUser);
            shopUserService.Save();

            var ward = wardService.GetByID((int)user.WardID);
            var district = districtService.GetByID(ward.DistrictID);
            var city = cityService.GetByID(district.CityID);
            //Lựa chọn trường sẽ trả ra view
            var data = new
            {
                ID = shopUser.ID,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                DetailAddress = user.DetailAddress,
                City = city.Name,
                District = district.Name,
                Ward = ward.Name,
                Sex=user.Sex,
                Role=shopUser.RoleID,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// POST-Delete
        /// </summary>
        /// <param name="shopUserID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int shopUserID)
        {
            shopUserService.Delete(shopUserID, true);
            shopUserService.Save();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Post:Edit ShopUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="roleDescription"></param>
        /// <param name="shopUserID"></param>
        /// <returns>ShopUser-editted</returns>
        [HttpPost]
        public JsonResult Edit(User user, int role, string roleDescription, int shopUserID)
        {
            userService.Update(user);
            var shopUser = shopUserService.GetByID(shopUserID);
            shopUserService.Update(shopUser, role, roleDescription);
            //userService.Save();
            shopUserService.Save();

            var ward = wardService.GetByID((int)user.WardID);
            var district = districtService.GetByID(ward.DistrictID);
            var city = cityService.GetByID(district.CityID);
            //Lựa chọn trường sẽ trả ra view
            var data = new
            {
                ID = shopUser.ID,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                DetailAddress = user.DetailAddress,
                City = city.Name,
                District = district.Name,
                Ward = ward.Name,
                Sex = user.Sex,
                Role = shopUser.RoleID,
            }; ;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET:Deleted Employee
        /// </summary>
        /// <returns>List<ShopUser>-Deleted</returns>
        //
        public JsonResult ViewDeleted()
        {
            var data = shopUserService.GetShopEmployeeDeleted((int)shopUserService.GetByID(3).ShopID)
                .Select(x => new {
                    ID = x.ID,
                    Name = x.User.Name,
                    Username = x.User.Username,
                    Email = x.User.Email,
                    DetailAddress = x.User.DetailAddress,
                    City = x.User.Ward.District.City.Name,
                    District = x.User.Ward.District.Name,
                    Ward = x.User.Ward.Name,
                    Sex = x.User.Sex,
                    Role = x.RoleID,
                }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET:Current Employee
        /// </summary>
        /// <returns>List<ShopUser>-Managed</returns>
        //
        public JsonResult ViewCurrent()
        {
            var data = shopUserService.GetShopEmployee((int)shopUserService.GetByID(3).ShopID)
                .Select(x => new {
                    ID = x.ID,
                    Name = x.User.Name,
                    Username = x.User.Username,
                    Email = x.User.Email,
                    DetailAddress = x.User.DetailAddress,
                    City = x.User.Ward.District.City.Name,
                    District = x.User.Ward.District.Name,
                    Ward = x.User.Ward.Name,
                    Sex = x.User.Sex,
                    Role =x.RoleID,
                }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check Username Is Unique
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userID"></param>
        /// <returns>bool</returns>
        [HttpPost]
        public bool IsUsernameUnique(string input, int? userID)
        {
            bool check = true;
            //(int)shopUserService.GetByID(3):số 3 sẽ được thay bằng userID đang đăng nhập khi bổ sung Login
            int shopID = (int)shopUserService.GetByID(3).ShopID;
            var model = shopUserService.GetAll()
                .Where(x => x.User.Username == input && x.ShopID == shopID && x.IsDelete == false).FirstOrDefault();
            var comparator = shopUserService.GetAll()
                .Where(x => x.User.Username == input && x.User.ID != userID && x.ShopID == shopID && x.IsDelete == false).FirstOrDefault();
            if (userID.HasValue == false || userID == 0)
            {
                if (model != null)
                {
                    check = false;
                }
                else
                {
                    //do nothing
                }
            }
            else
            {
                if (comparator != null)
                {
                    check = false;
                }
                else
                {
                    //do nothing
                }
            }
            return check;
        }

        /// <summary>
        /// Check Email Is Unique
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userID"></param>
        /// <returns>bool</returns>
        [HttpPost]
        public bool IsEmailUnique(string input, int? userID)
        {
            bool check = true;
            //(int)shopUserService.GetByID(3):số 3 sẽ được thay bằng userID đang đăng nhập khi bổ sung Login
            int shopID = (int)shopUserService.GetByID(3).ShopID;
            var model = shopUserService.GetAll()
                .Where(x => x.User.Email == input && x.ShopID == shopID && x.IsDelete == false).FirstOrDefault();
            var comparator = shopUserService.GetAll()
                .Where(x => x.User.Email == input && x.User.ID != userID && x.ShopID == shopID && x.IsDelete == false).FirstOrDefault();
            if (userID.HasValue == false || userID == 0)
            {
                if (model != null)
                {
                    check = false;
                }
                else
                {
                    //do nothing
                }
            }
            else
            {
                if (comparator != null)
                {
                    check = false;
                }
                else
                {
                    //do nothing
                }
            }
            return check;
        }

        /// <summary>
        /// Recover
        /// </summary>
        /// <param name="shopUserID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Recover(int shopUserID)
        {
            shopUserService.Recover(shopUserID);
            shopUserService.Save();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}