using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[action]")]
    [Route("[controller]/[action]")]
    public class HomeController : AdvancedController
    {
        [Route("~/")]
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
