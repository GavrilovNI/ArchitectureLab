using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index(string error)
        {
            return View(error);
        }
    }
}
