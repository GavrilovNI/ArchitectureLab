using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class Cart : Repository
    {
        public string UserId { get; private set; }

        public Cart(DataContext context, string userId) : base(context)
        {
            UserId = userId;
        }

        public CartItem GetItem(long itemId)
        {
            return new CartItem(Context, UserId, itemId);
        }

        public void Remove(long itemId)
        {
           new CartItem(Context, UserId, itemId).Count = 0;
        }

        public IQueryable<CartDbRow> GetAllDbRows()
        {
            return Context.Carts.Where(x => x.UserId == UserId);
        }

        public IQueryable<CartItem> GetAll()
        {
            return GetAllDbRows().Select(x => new CartItem(Context, UserId, x.ItemId));
        }

        public void Clear()
        {
            Context.Carts.RemoveRange(GetAllDbRows());
            Context.SaveChanges();
        }

        public bool CanBeApplied()
        {
            ProductRepository productRepository = new ProductRepository(Context);
            foreach(CartItem cartItem in GetAll().ToList())
            {
                Product? product = productRepository.Get(cartItem.ItemId);
                if(product == null)
                    return false;
                if (product.AvaliableAmount < cartItem.Count)
                    return false;
            }
            return true;
        }

        public void Fix()
        {
            ProductRepository productRepository = new ProductRepository(Context);
            foreach (CartItem cartItem in GetAll().ToList())
            {
                Product? product = productRepository.Get(cartItem.ItemId);
                if (product == null)
                    cartItem.Count = 0;
                if (product.AvaliableAmount < cartItem.Count)
                    cartItem.Count = product.AvaliableAmount;
            }
        }

        public void Apply()
        {
            BoughtCartRepository boughtCartRepository = new BoughtCartRepository(Context);
            ProductRepository productRepository = new ProductRepository(Context);
            BoughtCart boughtCart = new BoughtCart(this, productRepository);

            foreach(BoughtProduct boughtProduct in boughtCart.Products)
            {
                Product product = productRepository.Get(boughtProduct.ProductId);
                product.AvaliableAmount -= boughtProduct.Count;
                productRepository.Update(product);
            }

            boughtCartRepository.Add(boughtCart);
            Clear();
        }
    }
}
