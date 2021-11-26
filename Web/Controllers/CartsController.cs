using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;

namespace Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public CartController(DataContext dataContext, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Cart cart = new CartRepository(_dataContext).Get(UserId);
            IEnumerable<CartItemInfo> model;
            if(cart == null)
                model = new List<CartItemInfo>();
            else
                model = cart.CartItems.Select(x => new CartItemInfo(x, new ProductRepository(_dataContext))).ToList();

            return View(model);
        }

        public IActionResult AddItem(int itemId, int count)
        {
            if (count <= 0)
                count = 1;

            // TODO: check itemId

            new CartRepository(_dataContext).AddOrIncrease(UserId, new CartItem(itemId, count));

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItem(int itemId, int count)
        {
            if (count <= 0)
                count = 1;

            // TODO: check itemId

            new CartRepository(_dataContext).DecreaseOrRemove(UserId, new CartItem(itemId, count));

            return RedirectToAction("Index");
        }
    }
}
