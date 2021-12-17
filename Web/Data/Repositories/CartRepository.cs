using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repositories
{
    public class CartRepository : Repository
    {
        public CartRepository(DataContext context) : base(context)
        {
        }

        protected DbSet<CartItem> DbSet => Context.CartItems;

        public void Add(Cart cart)
        {
            foreach (var item in cart.Items)
            {
                DbSet.Add(item);
            }
            Context.SaveChanges();
        }

        public Cart Get(string userId)
        {
            List<CartItem> cartItems = DbSet.Where(x => x.CartId == userId).ToList();
            return new Cart(userId, cartItems);
        }

        public void Remove(string userId)
        {
            DbSet.RemoveRange(Get(userId).Items);
            Context.SaveChanges();
        }

        public void Update(Cart cart)
        {
            Remove(cart.UserId);
            Add(cart);
            Context.SaveChanges();
        }
    }
}
