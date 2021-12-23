using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;
using Web.Jwt;
using Web.ViewData;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = AuthOptions.AuthSchemes)]
    [Route("[controller]/[action]")]
    public class CartController : AdvancedController
    {
        private readonly DataContext _dataContext;

        private LoginModel LoginModel { get; set; }
        private string? UserId => GetUserId(LoginModel).Result;

        public CartController(DataContext dataContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(dataContext, userManager, signInManager)
        {
            _dataContext = dataContext;
        }

        [NonAction]
        private Cart GetCart()
        {
            CartRepository cartRepository = new CartRepository(_dataContext);
            return cartRepository.Get(UserId);
        }

        [HttpGet]
        [HttpGet("~/[controller]")]
        public IActionResult Index()
        {
            IEnumerable<CartItem> items = GetCart().Items;
            ProductRepository productRepository = new ProductRepository(_dataContext);
            List<ProductInfo> products = items.Select(x => new ProductInfo(productRepository, x)).ToList();
            var model = new CartIndexData(products);
            return ApiOrView(model);
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> Index([FromBody] LoginModel loginModel)
        {
            if(ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return Index();
        }

        [HttpGet]
        public IActionResult SetItemCount(long itemId, int count)
        {
            Product? product = new ProductRepository(_dataContext).Get(itemId);
            Cart cart = GetCart();
            CartItem cartItem = cart.GetItemOrCreate(itemId);

            if (product == null)
            {
                cartItem.Count = 0;
                return Error(HttpStatusCode.BadRequest, "product not found");
            }

            if (count < 0)
                return Error(HttpStatusCode.BadRequest, "count can't be less than 0");
            

            if (count > product.AvaliableAmount)
            {
                cartItem.Count = product.AvaliableAmount;
                return Error(HttpStatusCode.BadRequest, "not enough products");
            }

            cartItem.Count = count;
            cart.RemoveIfEmpty(cartItem.ItemId);
            CartRepository cartRepository = new CartRepository(_dataContext);
            cartRepository.Update(cart);


            return LocalRedirectApi("~/Cart/Index");
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> SetItemCount([FromBody] LoginModel loginModel, long itemId, int count)
        {
            if (ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return SetItemCount(itemId, count);
        }

        [HttpGet]
        public IActionResult AddItem(long itemId, int count)
        {
            CartItem cartItem = GetCart().GetItemOrCreate(itemId);
            return SetItemCount(itemId, cartItem.Count + count);
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> AddItem([FromBody] LoginModel loginModel, long itemId, int count)
        {
            if (ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return AddItem(itemId, count);
        }

        [HttpGet]
        public IActionResult RemoveItem(long itemId, int count)
        {
            CartItem cartItem = GetCart().GetItemOrCreate(itemId);
            return SetItemCount(itemId, cartItem.Count - count);
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> RemoveItem([FromBody] LoginModel loginModel, long itemId, int count)
        {
            if (ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return RemoveItem(itemId, count);
        }

        [Authorize]
        [HttpPost(Name = "Checkout")]
        public IActionResult Checkout(CartIndexData deliveryAddressHandler)
        {
            if (string.IsNullOrEmpty(deliveryAddressHandler.DeliveryAddress))
                return BadRequest();

            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);
            ProductRepository productRepository = new ProductRepository(_dataContext);

            CartRepository cartRepository = new CartRepository(_dataContext);

            Cart cart = cartRepository.Get(UserId);
            if (cart.CanBeApplied(productRepository) == false)
            {
                cart.Fix(productRepository);
                cartRepository.Update(cart);
                return Error(HttpStatusCode.BadRequest, "cart fixed");
            }

            cart.Apply(boughtCartRepository, productRepository, deliveryAddressHandler.DeliveryAddress);
            cartRepository.Remove(UserId);
            return LocalRedirectApi("~/Order");
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public IActionResult Checkout(CartIndexData deliveryAddressHandler, [FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
                LoginModel = loginModel;
            if (UserId == null)
                return Unauthorized();

            return Checkout(deliveryAddressHandler);
        }
    }
}
