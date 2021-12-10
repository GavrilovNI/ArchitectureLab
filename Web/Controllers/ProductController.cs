using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;

namespace Web.Controllers
{
    public class ProductController : AdvancedController
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            IQueryable<Product> products = new ProductRepository(_dataContext).GetAll().OrderBy(x => x.Name);
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

        [HttpGet]
        public IActionResult Edit(long productId)
        {
            Product? product = new ProductRepository(_dataContext).Get(productId);
            if (product == null)
                return Error(400, "product not found");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            new ProductRepository(_dataContext).Get(product.Id).Update(product);
            _dataContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            new ProductRepository(_dataContext).Add(product);
            return View();
        }
    }
}

