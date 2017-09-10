using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using CoffeeShop.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class DistrictController : Controller
    {
        IDistrictService _iDistrictService; //Declare interface IDistrictService.

        ///<summary>
        ///The class constructor.
        ///</summary>
        public DistrictController(IDistrictService iDis)
        {
            this._iDistrictService = iDis;
        }

        // GET: District.
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET:Load data for datatables.
        /// </summary>
        /// <returns>Json</returns>
        public ActionResult LoadData()
        {
            var list = _iDistrictService
                .GetAll()
                .Select(x => new DistrictViewModel {
                    ID = x.ID,
                    Name = x.Name,
                    Description = x.Description,
                    CityID = x.CityID,
                    NameCity = x.City.Name,
                    IsDelete = x.IsDelete });//create list DistrictViewModel.

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);//return json with data is list district.
        }

        /// <summary>.
        /// GET:Create partial list district.
        /// </summary>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult PartialListDistrict()
        {
            var list = _iDistrictService.GetAllIsDelete();
            return PartialView("_PartialListDistrict", list);
        }


        [HttpPost]
        public JsonResult GetDistrictByCityID(int id)
        {
            var list = _iDistrictService.GetDistrictByCityID(id).Select(x => new DistrictViewModel { ID = x.ID, Name = x.Name, Description = x.Description, CityID = x.CityID, NameCity = x.City.Name, IsDelete = x.IsDelete });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// POST:Insert.
        /// </summary>
        /// <param name="District object"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Insert(District district)
        {
            try
            {
                int rs = _iDistrictService.Insert(district);
                _iDistrictService.Save();
                return rs;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// POST:Update
        /// </summary>
        /// <param name="District object"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Update(District district)
        {
            try
            {
                int rs = _iDistrictService.Edit(district);
                if (rs == 1)
                {
                    _iDistrictService.Save();
                }
                return rs;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// POST:Delete
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                _iDistrictService.Delete1(id);
                _iDistrictService.Save();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// POST:Restore
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns>int</returns
        [HttpPost]
        public int Restore(int id)
        {
            try
            {
                _iDistrictService.Restore(id);
                _iDistrictService.Save();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}