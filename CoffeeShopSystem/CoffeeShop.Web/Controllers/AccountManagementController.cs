using CoffeeShop.Model.ModelEntity;

using CoffeeShop.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class AccountManagementController : Controller
    {

   
        IUserService _userService;
        IShopService _shopService;
        IWardService _wardService;
        IDistrictService _districtService;
        ICityService _cityService;
       



        public AccountManagementController(IUserService userService , IShopService shopService, IWardService wardService, IDistrictService  districtService ,ICityService cityService)
        {
            _userService = userService;
            _shopService = shopService;
        
            _wardService = wardService;
            _districtService = districtService;
            _cityService = cityService;
        }

        

        // GET: AccountManagement
        public ActionResult Index()
        {

            // AccountService asv = new AccountService(userRepository,unitOfWork);
            var lstUser = _userService.GetAll();  
            return View(lstUser.ToList());

            
        }


        public ActionResult CreateNew()
        {   
            //var lstBase = CityService.GetAll();
            return View();


        }

        [HttpPost]
        public ActionResult CreateNew(ApplicationUser u)
        {
           // IUserRepository userRepository = unitOfWork.GetUserRepo();
         //   AccountService asv = new AccountService(userRepository, unitOfWork);
            _userService.Add(u);
            return View();
        }

        public ActionResult Edit(int id)
        {
            //if (id.HasValue == false)
            //    return RedirectToAction("Index", "AccountManagement");

            //Userid has value
            //check if that id exist in db
            // IUserRepository userRepository = unitOfWork.GetUserRepo();
            // AccountService asv = new AccountService(userRepository, unitOfWork);
            ApplicationUser u = _userService.GetByID(id);
            if (u == null)
            {
                return RedirectToAction("Index", "AccountManagement");
            }

            return View(u);
                  
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUser u)
        {

       //     IUserRepository userRepository = unitOfWork.GetUserRepo();
         //   AccountService asv = new AccountService(userRepository, unitOfWork);
            _userService.Update(u);

            return 
            RedirectToAction("Index", "AccountManagement", new { result="EditSuccess" });
        }




        public JsonResult UpdateDistrictList(int? cityID)
        {   
            
            if (cityID.HasValue == false)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
           //var lstDist =  _districtService.DistrictListByCityID(cityID.Value);
            var lstDist = _districtService.GetDistrictByCityID(cityID.GetValueOrDefault(-1));

            if (lstDist == null)
           {
               return Json(null, JsonRequestBehavior.AllowGet);

           }

          return this.Json( new
                 {
                      Result = (from obj in lstDist select new { ID = obj.ID, Name = obj.Name })
                 }
                 , JsonRequestBehavior.AllowGet);
           
        }

        public JsonResult UpdateWardList(int? districtID)
        {   if (districtID.HasValue == false)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            
            //List <Ward> lstWard= _wardService.WardListByDistrictID(districtID);
            List<Ward> lstWard = _wardService.GetByDistrictID(districtID.GetValueOrDefault(-1)).ToList();

            if (lstWard == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
      
        return this.Json(new
           {
            Result = (from obj in lstWard select new { ID = obj.ID, Name = obj.Name })
           }
           , JsonRequestBehavior.AllowGet);
       }



        public JsonResult SearchByPhone(string phoneFilter)
        {
      

            var list = _userService.SearchByPhone(phoneFilter).ToList();
            
                if (list == null)
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
              
                return Json(list, JsonRequestBehavior.AllowGet);

            }



        public ActionResult Delete(int id)
        {

            ApplicationUser u = _userService.GetByID(id);
            if (u == null)
            {
                return RedirectToAction("Index", "AccountManagement");
            }

            // found user then soft delete
            
            _userService.Delete(id);
            return
            RedirectToAction("Index", "AccountManagement", new { result = "DeleteSuccess" });

        }

        
        public ActionResult PartialViewListShop()
        {
            List<Shop> lstShop = _shopService.GetAll().ToList();
           
            return PartialView(lstShop);
        }

        



    }
}
