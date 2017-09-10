using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using CoffeeShop.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class WardController : Controller
    {
        IWardService _iWardService;//Declare interface IWardService.

        ///<summary>
        ///The class constructor.
        ///</summary>
        public WardController(IWardService iWard)
        {
            this._iWardService = iWard;
        }

        // GET: Ward
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
            var list = _iWardService
                .GetAll()
                .Select(x => new WardViewModel {
                    ID = x.ID, Name = x.Name,
                    Description = x.Description,
                    DistrictID = x.DistrictID,
                    NameDistrict = x.District.Name,
                    IsDelete = x.IsDelete });//create list WardViewModel.

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);//return json with data is list ward.
        }

        /// <summary>
        /// POST:Insert
        /// </summary>
        /// <param name="Ward object"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Insert(Ward ward)
        {
            try
            {
                int rs = _iWardService.Insert(ward);//call service
                _iWardService.Save();//save changes
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
        /// <param name="Ward object"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Update(Ward ward)
        {
            try
            {
                int rs = _iWardService.Edit(ward);//call service
                if (rs == 1)
                {
                    _iWardService.Save();//save changes
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
        /// <param name="wardID"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                _iWardService.Delete1(id);//call service
                _iWardService.Save();//save changes
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// POST:Restore
        /// </summary>
        /// <param name="wardID"></param>
        /// <returns>int</returns>
        [HttpPost]
        public int Restore(int id)
        {
            try
            {
                _iWardService.Restore(id);//call service
                _iWardService.Save();//save changes
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}