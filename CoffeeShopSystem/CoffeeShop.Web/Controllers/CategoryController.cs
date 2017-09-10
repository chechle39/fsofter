using CoffeeShop.Data.Repositories;
using CoffeeShop.Model.ModelEntity;
using CoffeeShop.Service;
using System;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class CategoryController : Controller
    {
        IProductCategoryService  _category;
        /// <summary>
        /// Khoi tao
        /// </summary>
        /// <param name="category"></param>
        public CategoryController(IProductCategoryService category)
        {
             _category = category;
        }
        // GET: Category
        /// <summary>
        /// page index show list
        /// </summary>
        /// <returns>list view category</returns>
        public ActionResult Index()
        {
            var list = _category.GetAll();
            return View(list);
        }
        /// <summary>
        /// view su dung cho combobox loai san pham
        /// </summary>
        /// <returns>list</returns>
        public ActionResult ListCate()
        {
            var list = _category.GetAll();
            return View(list);
        }
        /// <summary>
        /// Them moi 1 loai san pham
        /// </summary>
        /// <param name="cate">Category</param>
        /// <returns>List view category</returns>
        // GET: Category/Create
        [HttpPost]
        public ActionResult Create(ProductCategory cate)
        {
            _category.Add(cate);
            _category.Save();
            return View();
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="cate">Category</param>
        /// <returns>View list</returns>
        [HttpPost]
        public ActionResult Edit(ProductCategory cate)
        {
            _category.Update(cate);
            _category.Save();
            return View();
        }
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id">Category</param>
        /// <returns>Json True or False</returns>
        public ActionResult Delete(int id)
        {
            try
            {
                //server side code
                _category.DeleteCate(id);
                _category.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet); ;
            }
        }
        /// <summary>
        /// Restore Category
        /// </summary>
        /// <param name="id">Category</param>
        /// <returns>list view</returns>
        public ActionResult Restore(int id)
        {
            _category.Restore(id);
            _category.Save();
            return View();
        }
    }
}
