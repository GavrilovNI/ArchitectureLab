using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public abstract class AdvancedController : Controller
    {
        public const string ApiPrefix = "api/";
        public const string DefaultApiHttpGetTemplate = "~/" + ApiPrefix + "[controller]/[action]";

        [NonAction]
        public IActionResult Error(int code, string message)
        {
            return RedirectToAction("Index", "Error", new { code = code, message = message } );
        }

        [NonAction]
        public IActionResult ApiOrView(object? model = null)
        {
            string? route = Request.Path.Value;
            if(route != null && route.ToLower().StartsWith("/" + ApiPrefix.ToLower()))
            {
                return Ok(model);
            }
            else
            {
                return View(model);
            }
        }
    }
}
