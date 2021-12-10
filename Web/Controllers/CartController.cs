using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;
using System.Linq;

namespace Web.Controllers
{
    [Authorize]
    public class CartController : AdvancedController
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public CartController(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<CartItem> items = new Cart(_dataContext, UserId).GetAll();
            ProductRepository productRepository = new ProductRepository(_dataContext);
            List<ProductInfo> model = items.Select(x => new ProductInfo(productRepository, x)).ToList();

            return View(model);
        }

        public IActionResult SetItemCount(long itemId, int count)
        {
            Product? product = new ProductRepository(_dataContext).Get(itemId);
            CartItem cartItem = new Cart(_dataContext, UserId).GetItem(itemId);

            if (product == null)
            {
                cartItem.Count = 0;
                return Error(400, "product not found");
            }

            if (count < 0)
                return Error(400, "count can't be less than 0");
            

            if (count > product.AvaliableAmount)
            {
                cartItem.Count = product.AvaliableAmount;
                return Error(400, "not enough products");
            }

            cartItem.Count = count;

            return RedirectToAction("Index");
        }

        public IActionResult AddItem(long itemId, int count)
        {
            CartItem cartItem = new Cart(_dataContext, UserId).GetItem(itemId);
            return SetItemCount(itemId, cartItem.Count + count);
        }

        public IActionResult RemoveItem(long itemId, int count)
        {
            CartItem cartItem = new Cart(_dataContext, UserId).GetItem(itemId);
            return SetItemCount(itemId, cartItem.Count - count);
        }
    }
}
