using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : AdvancedController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
