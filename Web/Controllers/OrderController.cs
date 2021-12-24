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
using Web.Utils;

namespace Web.Controllers
{
    [Authorize]
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

            if(cart.IsEveryItemHasStatus(PaidStatus.NotPaid) == false)
			{
                return Error(HttpStatusCode.BadRequest, "Order was paid or cancelled.");
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

        [HttpGet]
        public IActionResult CancelOrder(long cartId)
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);
            ProductRepository productRepository = new ProductRepository(_dataContext);
            BoughtCart? cart = boughtCartRepository.Get(cartId);
            if (cart == null || cart.UserId != UserId)
            {
                return Error(HttpStatusCode.BadRequest, "Access denied.");
            }

            if (cart.IsEveryItemHasStatus(PaidStatus.Paid) == false)
            {
                return Error(HttpStatusCode.BadRequest, "Order was not paid.");
            }

            cart.Cancel(boughtCartRepository, productRepository);
            return LocalRedirectApi("~/Order");
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate + "/{cartId}")]
        public async Task<IActionResult> CancelOrder([FromBody] LoginModel loginModel, long cartId)
        {
            if (ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return CancelOrder(cartId);
        }

    }
}
