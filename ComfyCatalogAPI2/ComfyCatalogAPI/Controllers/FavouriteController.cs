using Microsoft.AspNetCore.Mvc;

namespace ComfyCatalogAPI.Controllers
{
    public class FavouriteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
