using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Utils;

namespace Web.Controllers
{
    public class AccountController : AdvancedController
    {
        public AccountController(DataContext dataContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(dataContext, userManager, signInManager)
        {
        }

        public IActionResult Login(string ReturnUrl)
        {
            return LocalRedirect("~/Identity/Account/Login" + QueryString.Create("ReturnUrl", ReturnUrl));
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public IActionResult IsValid([FromBody]LoginModel loginModel)
        {
            bool valid = GetLoginModelUserId(loginModel).Result != null;
            return Ok(valid);
        }
    }
}
