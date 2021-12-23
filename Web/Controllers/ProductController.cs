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
using Web.Utils;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : AdvancedController
    {
        private const int PAGE_SIZE = 10;
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(dataContext, userManager, signInManager)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [HttpGet("~/[controller]")]
        [HttpGet(DefaultApiHttpGetTemplate)]
        public IActionResult Index(
                                   [FromQuery] ProductFilter filter,
                                   [FromQuery] ProductSorter sorter,
                                   [FromQuery] PageSelector<Product> pageSelector,
                                   [FromQuery] ProductSearcher searcher)
        {
            IQueryable<Product> products = new ProductRepository(_dataContext).GetAll();
            products = filter.Apply(products);
            products = sorter.Apply(products);
            products = pageSelector.Apply(products);
            products = searcher.Apply(products);

            List<ProductInfo> model;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Cart cart = new CartRepository(_dataContext).Get(userId);
                model = products.Select(x => new ProductInfo(x, cart.GetItemOrCreate(x.Id).Count)).ToList();
            }
            else
            {
                model = products.Select(x => new ProductInfo(x, 0)).ToList();
            }

            return ApiOrView(model);
        }

        [HttpGet(DefaultApiHttpGetTemplate+ "/{itemId}")]
        public IActionResult Info(int itemId)
        {
            IQueryable<Product> products = new ProductRepository(_dataContext).GetAll().Where(x => x.Id == itemId);
            ProductInfo model;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Cart cart = new CartRepository(_dataContext).Get(userId);
                model = products.Select(x => new ProductInfo(x, cart.GetItemOrCreate(x.Id).Count)).ToList().First();
            }
            else
            {
                model = products.Select(x => new ProductInfo(x, 0)).ToList().First();
            }

            return ApiOrView(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [HttpGet(DefaultApiHttpGetTemplate)]
        public IActionResult Edit(long itemId)
        {
            Product? product = new ProductRepository(_dataContext).Get(itemId);
            if (product == null)
                return Error(HttpStatusCode.BadRequest, "product not found");
            return ApiOrView(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, [FromBody] LoginModel loginModel)
        {
            var userId = await GetUserId(loginModel, "Admin");
            if (userId == null)
                return Unauthorized();

            new ProductRepository(_dataContext).Update(product);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(long itemId, [FromBody] LoginModel loginModel)
        {
            var userId = await GetUserId(loginModel, "Admin");
            if (userId == null)
                return Unauthorized();

            new ProductRepository(_dataContext).Remove(itemId);
            return LocalRedirectApi("~/Product");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, [FromBody] LoginModel loginModel)
        {
            var userId = await GetUserId(loginModel, "Admin");
            if (userId == null)
                return Unauthorized();

            new ProductRepository(_dataContext).Add(product);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Bill(long itemId)
        {
            var model = ProductBill.Create(itemId, new BoughtCartRepository(_dataContext));
            return ApiOrView(model);
        }

        [AllowAnonymous]
        [HttpPost(DefaultApiHttpGetTemplate)]
        public async Task<IActionResult> Bill([FromBody] LoginModel loginModel, long itemId)
        {
            var userId = await GetUserId(loginModel, "Admin");
            if (userId == null)
                return Unauthorized();

            return Bill(itemId);
        }
    }
}

