using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class PromotionController : Controller
    {
        //initialize service object
        static int itemperpage = 5;
        IPromotionService _promotionService;
        IEnumerable<Promotion> list;
        int TotalPage;

        /// <summary>
        /// This is PromotionController Constructer
        /// </summary>
        /// <param name="PromotionService"></param>
        public PromotionController(IPromotionService PromotionService)
        {
            _promotionService = PromotionService;

        }

        /// <summary>
        /// This method is used to load Promotion List
        /// </summary>
        /// <param name="Promotion"></param>
        /// <returns>Json</returns>
        public ActionResult Index(int page = 1)
        {
            list = _promotionService.GetActive();
            TotalPage = _promotionService.GetTotalPage(list.Count(), itemperpage);
            // totalitem, recordsPerPage
            ViewBag.Pages = TotalPage;
            ViewBag.CurPage = page;
            ViewBag.Select = "active";
            ViewBag.Count = list.Count();

            return View(_promotionService.Paging(list, itemperpage, page));
        }

        /// <summary>
        /// This method is used to load Promotion list by condition
        /// </summary>
        /// <param name="select"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult LoadByCondition(string select, int page = 1)
        {
            list = _promotionService.LoadByCondition(select);
            TotalPage = _promotionService.GetTotalPage(list.Count(), itemperpage);

            ViewBag.Select = select;
            ViewBag.Pages = TotalPage;
            ViewBag.CurPage = page;
            ViewBag.Count = list.Count();

            return View("Index", _promotionService.Paging(list, itemperpage, page));
        }

        /// <summary>
        /// This method is used to create a Promotion
        /// </summary>
        /// <param name="Promotion"></param>
        /// <returns>Json</returns>
        [HttpPost]
        public ActionResult Create(Promotion promotion)
        {
            _promotionService.Add(promotion);
            _promotionService.Save();
            int lastpage = _promotionService.GetTotalPage(_promotionService.GetActive().Count(), itemperpage);

            return RedirectToAction("Index", new { page = lastpage });
        }

        /// <summary>
        /// This method is used to get a Promotion detail
        /// </summary>
        /// <param name="Promotion"></param>
        /// <returns>Json</returns>
        public JsonResult GetById(int idToGet)
        {
            Promotion promotion = _promotionService.GetById(idToGet);

            return Json(promotion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used to edit a Promotion
        /// </summary>
        /// <param name="Promotion"></param>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult Edit(Promotion Promotion)
        {
            _promotionService.Update(Promotion);
            _promotionService.Save();

            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used to delete a Promotion
        /// </summary>
        /// <param name="idToDelete"></param>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult Delete(int idToDelete)
        {
            return Json(_promotionService.DeletePromotion(idToDelete), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used to recovery a Promotion
        /// </summary>
        /// <param name="idToRecovery"></param>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult Recovery(int idToRecovery)
        {
            return Json(_promotionService.RecoveryPromotion(idToRecovery), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used to Searchbasic
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult BasicSearch(string keyword, int page = 1)
        {
            list = _promotionService.BasicSearch(keyword);
            TotalPage = _promotionService.GetTotalPage(list.Count(), itemperpage);

            ViewBag.Pages = TotalPage;
            ViewBag.CurPage = page;
            ViewBag.Select = "active";

            ViewBag.keyword = keyword;
            ViewBag.Count = list.Count();

            return View("Index", _promotionService.Paging(list, itemperpage, page));
        }

        /// <summary>
        /// This method is used to Search Advanced
        /// </summary>
        /// <param name="promotion"></param>
        /// <returns></returns>
        public ActionResult AdvancedSearch(string Name, string StartDate, string EndDate, int page = 1)
        {
            list = _promotionService.AdvancedSearch(Name, StartDate, EndDate);
            TotalPage = _promotionService.GetTotalPage(list.Count(), itemperpage);

            ViewBag.Pages = TotalPage;
            ViewBag.CurPage = page;
            ViewBag.Select = "active";

            ViewBag.Name = Name;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.Count = list.Count();

            return View("Index", _promotionService.Paging(list, itemperpage, page));
        }
    }
}