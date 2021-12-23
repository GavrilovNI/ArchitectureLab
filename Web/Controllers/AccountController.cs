using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string ReturnUrl)
        {
            return LocalRedirect("~/Identity/Account/Login" + QueryString.Create("ReturnUrl", ReturnUrl));
        }
    }
}
