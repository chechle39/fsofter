using CoffeeShop.Service;
using System.Web.Mvc;

namespace CoffeeShop.Web.Controllers
{
    public class LoginController : Controller
    {
        ITableService _tableService;
        public LoginController(ITableService tableService)
        {
            this._tableService = tableService;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            var list = _tableService.GetAll();
            return View(list);
        }

    }
}