﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
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

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public CartController(DataContext dataContext)
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
        [HttpGet(DefaultApiHttpGetTemplate)]
        public IActionResult Index()
        {
            IEnumerable<CartItem> items = GetCart().Items;
            ProductRepository productRepository = new ProductRepository(_dataContext);
            List<ProductInfo> products = items.Select(x => new ProductInfo(productRepository, x)).ToList();
            var model = new CartIndexData(products);
            return ApiOrView(model);
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


            return LocalRedirect("~/Cart/Index");
        }

        [HttpGet]
        public IActionResult AddItem(long itemId, int count)
        {
            CartItem cartItem = GetCart().GetItemOrCreate(itemId);
            return SetItemCount(itemId, cartItem.Count + count);
        }

        [HttpGet]
        public IActionResult RemoveItem(long itemId, int count)
        {
            CartItem cartItem = GetCart().GetItemOrCreate(itemId);
            return SetItemCount(itemId, cartItem.Count - count);
        }

        [HttpPost(Name = "Checkout")]
        public IActionResult Checkout(CartIndexData deliveryAddressHandler)
        {
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
            return LocalRedirect("~/Order");
        }
    }
}
