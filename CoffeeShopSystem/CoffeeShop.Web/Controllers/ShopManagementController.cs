using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class ShopManagementController : Controller
    {

        //Khởi tạo service
        IShopService shopService;
        IDistrictService districtService;
        IWardService wardService;
        ICityService cityService;
        
        
        /// <summary>
        /// Hàm Khởi Tạo
        /// </summary>
        /// <param name="shopService"></param>
        /// <param name="districtService"></param>
        /// <param name="wardService"></param>
        /// <param name="cityService"></param>
        public ShopManagementController(IShopService shopService, IDistrictService districtService, IWardService wardService, ICityService cityService)
        {
            this.shopService = shopService;
            this.districtService = districtService;
            this.wardService = wardService;
            this.cityService = cityService;

        }







        
        /// <summary>
        /// Page Index
        /// </summary>
        /// <param name="sort"> "ID", "Name"</param>
        /// <param name="type"> "asc", "desc"</param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="option"> "Managing", "Deleted'</param>
        /// <returns></returns>
        public ActionResult Index(string sort = "ID", string type = "asc", int curPage = 1, int pageSize = 5, string option = "Managing")
        {


            int total; // count all the results base on option (Managing or Deleted)

            //
            ViewBag.IDSortType = shopService.ChangeSortType(type, sort == "ID");
            ViewBag.NameSortType = shopService.ChangeSortType(type, sort == "Name");
            ViewBag.AddressSortType = shopService.ChangeSortType(type, sort == "Address");
            ViewBag.DescriptionSortType = shopService.ChangeSortType(type, sort == "Description");
            ViewBag.Sort = sort;
            ViewBag.MyStyle = type;
            ViewBag.Option = option;

            List<Shop> lstShop;
            if (option == "Managing")
            {
                lstShop =  shopService.GetAllNonDeleteWithPaginationAndSort(sort, type, curPage, pageSize).ToList();
                total = shopService.GetAllNonDelete().Count();
            }

            else // Deleted
            {
                lstShop = shopService.GetAllDeletedWithPaginationAndSort(sort, type, curPage, pageSize).ToList();
                total = shopService.GetAllDeleted().Count();
            }
            int nPages = total / pageSize;

            if (total % pageSize > 0)
            {
                nPages++;
            }

            ViewBag.Pages = nPages;
            ViewBag.curPage = curPage;
            ViewBag.NextPage = curPage + 1;
            ViewBag.PrevPage = curPage - 1;
            return View(lstShop.ToList());


        }

        

        /// <summary>
        /// Creating new shop
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNew(Shop s)
        {

            bool result = shopService.CreateNew(s);
            if (result)
            {
                return RedirectToAction("Index", "ShopManagement", new { result = "AddSuccess" });
            }

            return RedirectToAction("Index", "ShopManagement", new { result = "AddFailed" });
        }

        




        /// <summary>
        /// Edit a shop
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Shop s)
        {


            bool result = shopService.Update(s);
            if (result)
            {
                return RedirectToAction("Index", "ShopManagement", new { result = "EditSuccess" });
            }

            return RedirectToAction("Index", "ShopManagement", new { result = "EditFailed" });
        }




        /// <summary>
        /// Update district combobox base on new cityID 
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns>Json</returns>
        public JsonResult UpdateDistrictList(int? cityID)
        {

            if (cityID.HasValue == false)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var lstDist = districtService.GetByCityID(cityID.GetValueOrDefault(-1)).ToList();

            if (lstDist == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);

            }

            return this.Json(new
            {
                Result = (from obj in lstDist select new { ID = obj.ID, Name = obj.Name })
            }
                   , JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Update ward combobox base on new districtID
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns>Json</returns>
        public JsonResult UpdateWardList(int? districtID)
        {
            if (districtID.HasValue == false)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            List<Ward> lstWard = wardService.GetByDistrictID(districtID.GetValueOrDefault(-1)).ToList();

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



        /// <summary>
        /// Search a shop base on its name or description
        /// </summary>
        /// <param name="Filter"></param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sort"></param>
        /// <param name="type"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public ActionResult SearchByNameOrDescription(string filter, int curPage = 1, int pageSize = 5, string sort = "ID", string type = "asc", string option = "Managing")
        {



            var listPaging = shopService.SearchByNameOrDescription(filter, curPage, pageSize, sort, type, option).ToList();


            var list = shopService.SearchByNameOrDescription(filter, 1, 100, sort, type, option).ToList(); // curpage =1 và pagesize rất lớn để skip 0 và take all
            if (list == null)
            {
                return PartialView("SearchByNameOrDescription", null);
            }


            int nPages = list.Count / pageSize;

            if (list.Count % pageSize > 0)
            {
                nPages++;
            }

            ViewBag.Pages = nPages;
            ViewBag.Filter = filter;
            ViewBag.curPage = curPage;
            ViewBag.Sort = sort;
            ViewBag.MyStyle = type;
            ViewBag.Option = option;
            ViewBag.PrevPage = ViewBag.curPage - 1;
            ViewBag.NextPage = ViewBag.curPage + 1;


            return PartialView("SearchByNameOrDescription", listPaging);




        }



        /// <summary>
        /// Delete a shop
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            //check if that id exist in db

            Shop s = shopService.GetShopByID(id);
            if (s == null)
            {
                return RedirectToAction("Index", "AccountManagement", new { result = "DeleteFailed" });
            }

            // found shop then soft delete

            bool result = shopService.SoftDelete(s);
            if (result)
            {
                return RedirectToAction("Index", "ShopManagement", new { result = "DeleteSuccess" });
            }
            return RedirectToAction("Index", "ShopManagement", new { result = "DeleteFailed" });

        }


        /// <summary>
        /// Recover a shop
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Recover(int id)
        {
            //check if that id exist in db

            Shop s = shopService.GetShopByID(id);
            if (s == null)
            {
                return RedirectToAction("Index", "AccountManagement", new { result = "RecoverFailed" });
            }

            // found shop then recover

            bool result = shopService.Recover(s);
            if (result)
            {
                return RedirectToAction("Index", "ShopManagement", new { result = "RecoverSuccess" });
            }
            return RedirectToAction("Index", "ShopManagement", new { result = "RecoverFailed" });

        }





        /// <summary>
        /// Display Modal Add New Shop
        /// </summary>
        /// <returns></returns>
        public ActionResult PartialViewAddShop()
        {
            return PartialView();
        }



        /// <summary>
        /// Display Modal Edit A Shop
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PartialViewEditShop(int id)
        {


            Shop s = shopService.GetShopByID(id);



            return PartialView(s);
        }




        /// <summary>
        /// Display combobox City
        /// </summary>
        /// <param name="wardID"></param>
        /// <returns></returns>
        public ActionResult PartialViewListCity(int? wardID)
        {
            List<City> lstCity;
            if (wardID == null)
            {
                lstCity = cityService.GetAll().ToList();
            }

            else
            {
                Ward w = wardService.GetSingleById(wardID.GetValueOrDefault(-1));
                District d = districtService.GetByID(w.DistrictID);
                lstCity = cityService.GetAll().ToList();
                ViewBag.CityID = d.CityID;
            }

            return PartialView(lstCity);
        }


        /// <summary>
        /// Display combobox Ward
        /// </summary>
        /// <param name="WardID"></param>
        /// <returns></returns>
        public ActionResult PartialViewListWard(int? WardID)
        {
            List<Ward> lstWard;
            if (WardID == null)
            {
                District firstDistrict = districtService.GetAll().FirstOrDefault();

                lstWard = wardService.GetByDistrictID(firstDistrict.ID).ToList();
            }

            else
            {
                Ward w = wardService.GetByID(WardID.GetValueOrDefault(-1));
                District d = districtService.GetByID(w.DistrictID);
                lstWard = wardService.GetByDistrictID(d.ID).ToList();
                ViewBag.WardID = WardID;
            }

            return PartialView(lstWard);
        }


        /// <summary>
        /// Display combobox district
        /// </summary>
        /// <param name="WardID"></param>
        /// <returns></returns>
        public ActionResult PartialViewListDistrict(int? WardID)
        {
            List<District> lstDistrict;
            if (WardID == null)
            {
                City firstCity = cityService.GetAll().FirstOrDefault();
                lstDistrict = districtService.GetByCityID(firstCity.ID).ToList();
            }
            else
            {
                Ward w = wardService.GetByID(WardID.GetValueOrDefault(-1));
                District d = districtService.GetByID(w.DistrictID);


                lstDistrict = districtService.GetByCityID(d.CityID).ToList();
                ViewBag.DistrictID = w.DistrictID;
            }

            return PartialView(lstDistrict);
        }


        public bool IsUniqueName(string input, int shopID)
        {
            return shopService.IsUniqueName(input, shopID);
        }
    }
}