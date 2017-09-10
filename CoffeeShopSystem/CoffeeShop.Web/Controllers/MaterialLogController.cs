using System.Linq;
using System.Web.Mvc;
using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System.Collections.Generic;
using CoffeeShop.Model;

namespace CoffeeShop.Web.Controllers
{
    public class MaterialLogController : Controller
    {
        private IMaterialLogService mLogService;
        private IMaterialService materialService;

        public MaterialLogController(IMaterialLogService mLogService, IMaterialService mateService)
        {
            this.mLogService = mLogService;
            this.materialService = mateService;
        }

        // GET: MaterialLog
        public ActionResult Index()
        {
            ViewBag.MateList = materialService.GetMulti(x => x.IsDelete == false);

            return View(mLogService.GetMulti(x => x.IsDelete == false));
        }

        /// <summary>
        /// return all records of MaterialLog in database
        /// </summary>
        /// <returns></returns>
        public JsonResult LoadData()
        {
            // flat result
            var result = new { items = mLogService.Flat(mLogService.GetAll()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        // POST: MaterialLog/Create
        /// <summary>
        /// create new MaterialLog item and return it's value if success
        /// </summary>
        /// <param name="materialLog">the item to be created</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Create(MaterialLog materialLog)
        {
            var result = mLogService.Add(materialLog);
            return Json(mLogService.Flat(result), JsonRequestBehavior.AllowGet);
        }
        
        // GET: MaterialLog/Edit/5
        /// <summary>
        /// get detail of materiallog item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Detail(int? id)
        {
            if (id != null)
            {
                var result = mLogService.GetSingleById(id.Value);

                return Json(mLogService.Flat(result), JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        
        // POST: MaterialLog/Edit/5
        /// <summary>
        /// update materiallog detail
        /// </summary>
        /// <param name="materialLog"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(MaterialLog materialLog)
        {
            var result =  mLogService.Update(materialLog);
            return Json(mLogService.Flat(result), JsonRequestBehavior.AllowGet);
        }

        // POST: MaterialLog/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = mLogService.Delete(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(MaterialLogSearchViewModel model)
        {
            var result = mLogService.Search(model);

            return Json(mLogService.Flat(result), JsonRequestBehavior.AllowGet);
        }
        
    }
}
