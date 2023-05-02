using Microsoft.AspNetCore.Mvc;

namespace ComfyCatalogAPI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
