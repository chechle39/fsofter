using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    /// <summary>
    /// Manage GroupTable event, action
    /// </summary>
    public class GroupTableController : Controller
    {
        // GET: GroupTable
        IGroupTableService groupTableService;
        public GroupTableController(IGroupTableService groupTableService)
        {
            this.groupTableService = groupTableService;
        }

        /// <summary>
        /// View quản lý
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Search()
        {
            return View(this.groupTableService.GetByShop(1));
        }

        /// <summary>
        /// Cập nhật grouptable
        /// </summary>
        /// <param name="groupTable">GroupTable</param>
        /// <returns>Json<bool></returns>
        [HttpPost]
        public ActionResult Edit(GroupTable groupTable)
        {
            groupTableService.Update(groupTable);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xóa grouptable
        /// </summary>
        /// <param name="ID">int</param>
        /// <returns>Json<bool></returns>
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            return Json(groupTableService.Delete(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Thêm grouptable
        /// </summary>
        /// <param name="groupTable">GroupTable</param>
        /// <returns>Json<ID, Name, Surcharge, TableCount, Description></returns>
        [HttpPost]
        public ActionResult Add(GroupTable groupTable)
        {
            groupTable.ShopID = 1;
            groupTableService.Add(groupTable);
            groupTableService.Save();
            var result = new
            {
                ID = groupTable.ID,
                Name = groupTable.Name,
                Surcharge = groupTable.Surcharge,
                TableCount = groupTable.Table.Count,
                Description = groupTable.Description };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Search theo tùy chọn (đã xóa <> chưa xóa)
        /// </summary>
        /// <param name="delete">bool</param>
        /// <returns>Json<List<data>, bool></returns>
        [HttpPost]
        public ActionResult SearchCondition(bool delete)
        {
            var resultToReturn = new
            {
                data = groupTableService.SearchCondition(delete),
                delete = delete
            };
            return Json(resultToReturn, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Phục hồi grouptable đã xóa
        /// </summary>
        /// <param name="ID">int</param>
        /// <returns>Json<bool></returns>
        [HttpPost]
        public ActionResult Recover(int ID)
        {
            return Json(groupTableService.Recover(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Search nâng cao
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="fromSurcharge">decimal</param>
        /// <param name="toSurcharge">decimal</param>
        /// <param name="delete">bool</param>
        /// <returns>Json<List<data>, bool></returns>
        public ActionResult SearchAdvanced(string name, decimal fromSurcharge, decimal toSurcharge, bool delete)
        {
            //Gọi service SearchAdvanced
            var resultToReturn = new
            {
                data = groupTableService.SearchAdvanced(name, fromSurcharge, toSurcharge, delete),
                delete = delete
            };
            return Json(resultToReturn, JsonRequestBehavior.AllowGet);
        }
    }
}