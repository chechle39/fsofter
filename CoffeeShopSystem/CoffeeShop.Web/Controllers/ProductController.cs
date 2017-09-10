using System;
using System.Web.Mvc;
using CoffeeShop.Service;
using CoffeeShop.Model.ModelEntity;

namespace CoffeeShop.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        IProductService  _productService ;
        IProductCategoryService _cateService;
       /// <summary>
       /// Khởi tạo
       /// </summary>
       /// <param name="productService"></param>
       /// <param name="categoryService"></param>
        public ProductController(IProductService productService , IProductCategoryService categoryService)
        {
            //Create 2 Service
            this._productService = productService;
            this._cateService = categoryService;
        }
        /// <summary>
        /// View Index
        /// </summary>
        /// <returns>View Index</returns>
        public ActionResult Index()
        {
            var list = _productService.GetAll();
            return View(list);
        }
       
        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>view list product</returns>
        // GET: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            _productService.Add(product);
            _productService.Save();
            return View();
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>view list product</returns>
        // GET: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            _productService.Update(product);
            _productService.Save();
            return View();
        }
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Product</param>
        /// <returns>Json True or False</returns>
        public ActionResult Delete(int id)
        {
            try
            {
                //server side code
                _productService.DeleteProduct(id);
                _productService.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet); ;
            }
        }
        /// <summary>
        /// Restore product
        /// </summary>
        /// <param name="id">Product</param>
        /// <returns>list view </returns>
        public ActionResult Restore(int id)
        {

            _productService.RestoreProduct(id);
            _productService.Save();
            return View();
        }

    }
}
