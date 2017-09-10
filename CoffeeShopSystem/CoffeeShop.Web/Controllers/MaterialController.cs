using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using CoffeeShop.Web.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Controllers
{
    public class MaterialController : Controller
    {
        private IMaterialService materialService;
        private IMaterialCategoryService materialCategoryService;

        /// <summary>
        /// Khoi tao controller
        /// </summary>
        /// <param name="mateService"></param>
        /// <param name="mateCateService"></param>
        public MaterialController(IMaterialService mateService, IMaterialCategoryService mateCateService)
        {
            this.materialService = mateService;
            this.materialCategoryService = mateCateService;
        }

        /// <summary>
        /// Hiện thị index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Lay danh sach material category cho combobox trong popup add va edit
            ViewBag.MateCateList = materialCategoryService.GetAll();
            return View();
        }

        /// <summary>
        /// Hàm load trang chính
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="status">trạng thái đã xóa hay chưa xóa</param>
        /// <param name="page">vị trí trang hiện tại</param>
        /// <param name="pageSize">số dòng mỗi trang</param>
        /// <returns>Trả về danh sách material theo điều kiện</returns>
        public ActionResult LoadData(string keyword, int status, int page, int pageSize = 5)
        {
            int totalRow = 0;

            var listMaterial = materialService.GetSearchStatusPaging(keyword, status, page, pageSize, out totalRow).Select(x => new MaterialViewModel
            {
                ID = x.ID,
                CategoryID = x.CategoryID,
                CategoryName = x.MaterialCategory.Name,
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                Description = x.Description,
                Inventory = x.Inventory,
                IsDelete = x.IsDelete,
                UnitPrice = x.UnitPrice
            });

            return Json(new
            {
                data = listMaterial, //danh sach Material
                total = totalRow, //Tỗng số record được truy vấn. để tính ra bao nhiêu trang.
                status = true //trạng thái
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gọi khi click submit form. Lưu hành động thêm hoặc sửa xuống database.
        /// </summary>
        /// <param name="strMaterial">chuỗi json 1 material</param>
        /// <returns>trạng thái thành công hay thất bại, message lỗi nếu có</returns>
        public JsonResult SubmitForm(string strMaterial)
        {
            //Lấy material được truyền qua
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Material material = serializer.Deserialize<Material>(strMaterial);
            bool status = false;
            string message = string.Empty;
            bool action;

            if (material.ID == 0) //action là thêm khi có id = 0
            {
                //material.CreatedDate = DateTime.Now;
                material.IsDelete = false;
                materialService.Add(material);
                action = true;
            }
            else //action là sửa khi có id cụ thể
            {
                material.IsDelete = false;
                materialService.Update(material);
                action = false;
            }

            //commit update database
            try
            {
                materialService.Save();
                status = true;
            }
            catch (SqlException ex)
            {
                status = false;
                message = ex.Message;
            }

            return Json(new
            {
                status = status, //trạng thái thành công status = true và ngược lại
                message = message, //thông báo lỗi
                action = action //xác thực hành động vừa làm là thêm hay sửa
            });
        }

        /// <summary>
        /// Lấy ra 1 material để hiện th5 view edit.
        /// </summary>
        /// <param name="id">id của record được chọn vào</param>
        /// <returns>1 material có ID = id tương ứng</returns>
        public JsonResult GetDetailEdit(int id)
        {
            Material material = materialService.GetSingleByID(id);
            MaterialViewModel materialVM = new MaterialViewModel()
            {
                ID = material.ID,
                CategoryID = material.CategoryID,
                CategoryName = material.MaterialCategory.Name,
                Name = material.Name,
                CreatedDate = material.CreatedDate,
                Description = material.Description,
                Inventory = material.Inventory,
                IsDelete = material.IsDelete,
                UnitPrice = material.UnitPrice
            };

            return Json(new
            {
                material = materialVM,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gọi khi xóa 1 material
        /// </summary>
        /// <param name="id">id của record được chọn vào</param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            string message = string.Empty;
            bool status;
            materialService.Delete(id);
            try
            {
                materialService.Save();
                status = true;
            }
            catch (SqlException sEx)
            {
                message = sEx.Message;
                status = false;
            }

            return Json(new
            {
                status = status,
                message = message
            }, JsonRequestBehavior.AllowGet);
        }
    }
}