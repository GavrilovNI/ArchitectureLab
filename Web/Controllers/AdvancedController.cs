using Database.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Jwt;

namespace Web.Controllers
{
    public abstract class AdvancedController : Controller
    {
        public const string ApiPrefix = "api/";
        public const string DefaultApiHttpGetTemplate = "~/" + ApiPrefix + "[controller]/[action]";

        private DataContext _dataContext;
        private UserManager<User>? _userManager;
        private SignInManager<User>? _signInManager;

        public AdvancedController()
        {
            _dataContext = null;
            _userManager = null;
            _signInManager = null;
        }

        public AdvancedController(DataContext dataContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [NonAction]
        public IActionResult Error(HttpStatusCode code, string message)
        {
            return RedirectToAction("Index", "Error", new { code = (int)code, message = message } );
        }

        [NonAction]
        public bool IsApi()
        {
            string? route = Request.Path.Value;
            return route != null && route.ToLower().StartsWith("/" + ApiPrefix.ToLower());
        }

        [NonAction]
        public IActionResult LocalRedirectApi(string message)
        {
            if(IsApi())
            {
                if (message.StartsWith("~/"))
                    return Ok();
                else if (message[0] == '/' && message.ToLower().StartsWith("/" + ApiPrefix.ToLower()) == false)
                    return Ok();
            }
            return LocalRedirect(message);
        }

        [NonAction]
        public IActionResult ApiOrView(object? model = null)
        {
            if(IsApi())
            {
                return Ok(model);
            }
            else
            {
                return View(model);
            }
        }

        [NonAction]
        protected async Task<string?> GetUserId(LoginModel? loginModel, params string[] roles)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
                return userId;

            return await GetLoginModelUserId(loginModel);
        }

        [NonAction]
        protected async Task<string?> GetLoginModelUserId(LoginModel? loginModel, params string[] roles)
        {
            if (loginModel == null)
                return null;

            var user = await _userManager!.FindByNameAsync(loginModel.Email);
            if (user == null)
                return null;
            var result = await _signInManager!.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (result.Succeeded)
            {
                UserRepository userRepository = new UserRepository(_dataContext);
                bool hasRole = false;
                foreach (var role in roles)
                {
                    if (userRepository.HasRole(user, role) == true)
                    {
                        hasRole = true;
                        break;
                    }
                }
                if (roles.Length != 0 && hasRole == false)
                    return null;
                return user.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
