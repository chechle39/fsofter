using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using CoffeeShop.Web.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CoffeeShop.Web.Controllers
{
    public class MaterialCategoryController : Controller
    {
        // GET: MaterialCategory
        private IMaterialCategoryService materialCategoryService;

        public MaterialCategoryController(IMaterialCategoryService mateCateService)
        {
            this.materialCategoryService = mateCateService;
        }

        // GET: Material
        public ActionResult Index()
        {
            return View();
        }

        //Hàm duoc ajax gọi khi load trang
        public ActionResult LoadData(string keyword, int status, int page, int pageSize = 5)
        {
            int totalRow = 0;
            var listMaterialCategory = materialCategoryService.GetSearchStatusPaging(keyword, status, page, pageSize, out totalRow).Select(x => new MaterialCategoryViewModel { ID = x.ID, Name = x.Name, CreatedDate = x.CreatedDate, Description = x.Description,IsDelete = x.IsDelete});

            return Json(new
            {
                data = listMaterialCategory, //danh sach MaterialCategory
                total = totalRow, //Tỗng số record được truy vấn
                status = true //trạng thái
            }, JsonRequestBehavior.AllowGet);
        }

        //Action được ajax gọi khi click submid trên popup
        public JsonResult SubmitForm(string strMaterial)
        {
            //Lấy material được truyền qua
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            MaterialCategory materialCategory = serializer.Deserialize<MaterialCategory>(strMaterial);

            bool status = false;
            string message = string.Empty;
            bool action;
            //action là thêm khi có id = 0
            if (materialCategory.ID == 0)
            {
                //material.CreatedDate = DateTime.Now;
                materialCategory.IsDelete = false;
                materialCategoryService.Add(materialCategory);
                action = true;
            }
            else //action là sửa khi có id cụ thể
            {
                materialCategory.IsDelete = false;
                materialCategoryService.Update(materialCategory);
                action = false;
            }

            //update xuống database
            try
            {
                materialCategoryService.Save();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }

            return Json(new
            {
                status = status,
                message = message,
                action = action
            });
        }

        public JsonResult GetDetailEdit(int id)
        {
            MaterialCategory materialCategory = materialCategoryService.GetSingleByID(id);
            return Json(new
            {
                materialCategory = materialCategory,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            string message = string.Empty;
            bool status;
            materialCategoryService.Delete(id);
            try
            {
                materialCategoryService.Save();
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