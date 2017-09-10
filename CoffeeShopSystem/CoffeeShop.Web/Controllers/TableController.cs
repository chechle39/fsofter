using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    /// <summary>
    /// Manage table
    /// </summary>
    public class TableController : Controller
    {
        // GET: Table
        ITableService tableService;
        IGroupTableService groupTableService;
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="tableService"></param>
        /// <param name="groupTableService"></param>
        public TableController(ITableService tableService, IGroupTableService groupTableService)
        {
            //Create 2 service
            this.tableService = tableService;
            this.groupTableService = groupTableService;
        }

        /// <summary>
        /// View index
        /// </summary>
        /// <returns>List<Table>, List<ViewBag.GroupTable></returns>
        public ActionResult Search()
        {
            //Gọi service GetByShop
            ViewBag.GroupTable = this.groupTableService.GetByShop(1);
            return View(this.tableService.GetByShop(1)); //Trả về danh sách table của shop
        }

        /// <summary>
        /// Cập nhật table
        /// </summary>
        /// <param name="table">Table</param>
        /// <returns>Json</returns>
        [HttpPost]
        public ActionResult Edit(Table table)
        {
            //Gọi service Update
            tableService.Update(table);
            //Save dữ liệu
            tableService.Save();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Thêm table
        /// </summary>
        /// <param name="table">Table</param>
        /// <returns>Json(ID, Name, GroupTableName, GroupTableID, OrderCount, Des)</returns>
        [HttpPost]
        public ActionResult Add(Table table)
        {
            //Gọi service Add
            tableService.Add(table);
            //Save
            tableService.Save();
            //Gọi service GetByID của grouptable lấy thông tin của group
            var resultGroupTable = groupTableService.GetSingleById(table.GroupTableID);
            var data = new
            {
                ID = table.ID,
                Name = table.Name,
                GroupTableName = resultGroupTable.Name,
                GroupTableID = resultGroupTable.ID,
                OrderCount = 0, Des = table.Description
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xóa table
        /// </summary>
        /// <param name="ID">int</param>
        /// <returns>Json<true, false></returns>
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            //Gọi service Delele
            return Json(tableService.Delete(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Search theo tình trạng của option
        /// </summary>
        /// <param name="option">bool</param>
        /// <returns>Json(List<data>, bool>)</returns>
        [HttpPost]
        public ActionResult SearchCondition(bool delete)
        {
            //Gọi service SearchCondition
            //gán kết quả trả về
            var resultToReturn = new
            {
                data = tableService.SearchCondition(delete),
                delete = delete
            };
            return Json(resultToReturn, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Phục hồi tình trạng xóa của Table
        /// </summary>
        /// <param name="ID">int</param>
        /// <returns>Json<true, false></returns>
        [HttpPost]
        public ActionResult Recover(int ID)
        {
            //Gọi service Recover
            return Json(tableService.Recover(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Search nâng cao
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="groupTableID">int</param>
        /// <param name="delete">bool</param>
        /// <returns>Json(List<data>, bool)</returns>
        [HttpPost]
        public ActionResult SearchAdvanced(string name, int groupTableID, bool delete)
        {
            var resultToReturn = new
            {
                data = tableService.SearchAdvanced(name, groupTableID, delete),
                delete = delete
            };
            //Gọi service SearchAdvanced
            return Json(resultToReturn, JsonRequestBehavior.AllowGet);
        }
    }
}