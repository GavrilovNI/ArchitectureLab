using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public abstract class AdvancedController : Controller
    {
        public IActionResult Error(int code, string message)
        {
            return RedirectToAction("Index", "Error", new { code = code, message = message } );
        }
    }
}
