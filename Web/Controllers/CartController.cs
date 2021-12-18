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

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public CartController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private Cart GetCart()
        {
            CartRepository cartRepository = new CartRepository(_dataContext);
            return cartRepository.Get(UserId);
        }

        public IActionResult Index()
        {
            IEnumerable<CartItem> items = GetCart().Items;
            ProductRepository productRepository = new ProductRepository(_dataContext);
            List<ProductInfo> model = items.Select(x => new ProductInfo(productRepository, x)).ToList();

            return View(model);
        }

        public IActionResult SetItemCount(long itemId, int count)
        {
            Product? product = new ProductRepository(_dataContext).Get(itemId);
            Cart cart = GetCart();
            CartItem cartItem = cart.GetItemOrCreate(itemId);

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
            cart.RemoveIfEmpty(cartItem.ItemId);
            CartRepository cartRepository = new CartRepository(_dataContext);
            cartRepository.Update(cart);
            

            return RedirectToAction("Index");
        }

        public IActionResult AddItem(long itemId, int count)
        {
            CartItem cartItem = GetCart().GetItemOrCreate(itemId);
            return SetItemCount(itemId, cartItem.Count + count);
        }

        public IActionResult RemoveItem(long itemId, int count)
        {
            CartItem cartItem = GetCart().GetItemOrCreate(itemId);
            return SetItemCount(itemId, cartItem.Count - count);
        }

        public IActionResult Apply()
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(_dataContext);
            ProductRepository productRepository = new ProductRepository(_dataContext);
            Cart cart = new Cart(UserId);
            if (cart.CanBeApplied(productRepository) == false)
            {
                cart.Fix(productRepository);
                return Error(400, "cart fixed");
            }

            cart.Apply(boughtCartRepository, productRepository);
            CartRepository cartRepository = new CartRepository(_dataContext);
            cartRepository.Remove(UserId);
            return RedirectToAction("Index", "Order");
        }
    }
}
