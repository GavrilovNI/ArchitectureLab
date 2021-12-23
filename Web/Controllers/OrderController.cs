using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;
using Web.Jwt;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = AuthOptions.AuthSchemes)]
    [Route("[controller]/[action]")]
    public class OrderController : AdvancedController
    {
        private readonly DataContext _dataContext;

        private LoginModel LoginModel { get; set; }
        private string? UserId => GetUserId(LoginModel).Result;

        public OrderController(DataContext dataContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(dataContext, userManager, signInManager)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [HttpGet("~/[controller]")]
        public IActionResult Index()
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);

            List<BoughtCart> boughtCarts = boughtCartRepository.GetAll()
                                                               .Include(c => c.BoughtProducts)
                                                               .ThenInclude(p => p.Product)
                                                               .Where(c => c.UserId == UserId)
                                                               .ToList();

            return ApiOrView(boughtCarts);
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> Index([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return Index();
        }

        [HttpGet]
        public IActionResult PayForOrder(long cartId)
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);
            BoughtCart cart = boughtCartRepository.Get(cartId);
            if (cart == null || cart.UserId != UserId)
            {
                return Error(HttpStatusCode.BadRequest, "Access denied.");
            }

            if(cart.IsFullyPaid())
            {
                return Error(HttpStatusCode.BadRequest, "Order is fully paid.");
            }


            cart.SetPaidStatusForAllProducts(PaidStatus.Paid);
            boughtCartRepository.Update(cart);
            return LocalRedirectApi("~/Order");
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate + "/{cartId}")]
        public async Task<IActionResult> PayForOrder([FromBody] LoginModel loginModel, long cartId)
        {
            if (ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return PayForOrder(cartId);
        }
    }
}
