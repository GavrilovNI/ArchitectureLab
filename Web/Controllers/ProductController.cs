using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;
using Web.Data.Utils;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : AdvancedController
    {
        private const int PAGE_SIZE = 10;
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [HttpGet(DefaultApiHttpGetTemplate)]
        public IActionResult Edit(long itemId)
        {
            Product? product = new ProductRepository(_dataContext).Get(itemId);
            if (product == null)
                return Error(400, "product not found");
            return ApiOrView(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            new ProductRepository(_dataContext).Update(product);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            new ProductRepository(_dataContext).Add(product);
            return View();
        }
    }
}

