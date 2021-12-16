using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;
using Web.Data.Utils;

namespace Web.Controllers
{
    public class ProductController : AdvancedController
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Index(string sortingKey, [FromQuery] ProductFilter filter)
        {
            string defaultSortingKey = "name";
            var sortingKeySelector = ProductSorter.GetKeySelector("") ?? ProductSorter.GetKeySelector(defaultSortingKey);


            IQueryable<Product> products = new ProductRepository(_dataContext).GetAll();
            products = filter.Apply(products);
            products = products.OrderBy(sortingKeySelector!);

            List<ProductInfo> model;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Cart cart = new Cart(_dataContext, userId);
                model = products.Select(x => new ProductInfo(x, cart.GetItem(x.Id).Count)).ToList();
            }
            else
            {
                model = products.Select(x => new ProductInfo(x, 0)).ToList();
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(long itemId)
        {
            Product? product = new ProductRepository(_dataContext).Get(itemId);
            if (product == null)
                return Error(400, "product not found");
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            new ProductRepository(_dataContext).Get(product.Id).Update(product);
            _dataContext.SaveChanges();
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

