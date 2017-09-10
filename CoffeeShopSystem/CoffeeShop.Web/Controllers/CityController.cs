using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class CityController : Controller
    {

        ICityService _iCityService;//Declare interface ICityService.

        ///<summary>
        ///The class constructor.
        ///</summary>
        public CityController(ICityService cityService)
        {
            this._iCityService = cityService;
        }

        // GET: City
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
            var list = _iCityService
                .GetAll()
                .Select(x => new City {
                    ID = x.ID,
                    Name = x.Name,
                    Description = x.Description,
                    IsDelete = x.IsDelete });//create list DistrictViewModel.

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);//return json with data is list city.
        }

        /// <summary>.
        /// GET:Create partial list city.
        /// </summary>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult PartialListCity()
        {
            var list = _iCityService.GetAllIsDelete();
            return PartialView("_PartialListCity", list);

        }

        /// <summary>
        /// POST:Insert.
        /// </summary>
        /// <param name="City object"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Insert(City city)
        {
            try
            {
                return _iCityService.Insert(city);//call service.
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// POST:Update.
        /// </summary>
        /// <param name="City object"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Update(City city)
        {
            try
            {
                int rs = _iCityService.Edit(city);//call service.
                if (rs == 1)
                {
                    _iCityService.Save();//save changes.
                }
                return rs;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// POST:Delete.
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                _iCityService.Delete1(id);//call service.
                _iCityService.Save();//save changes.
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// POST:Restore.
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Restore(int id)
        {
            try
            {
                _iCityService.Restore(id);//call service.
                _iCityService.Save();//save changes.
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}