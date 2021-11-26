using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repositories
{
    public class CartRepository : Repository
    {
        public CartRepository(DataContext context) : base(context)
        {

        }

        protected DbSet<CartDbRow> DbSet => Context.Carts;

        public Cart? Get(string userId)
        {
            IQueryable<CartDbRow> cartDbRows = DbSet.Where(x => x.UserId == userId);
            if (cartDbRows.Any() == false)
                return null;
            return new Cart(cartDbRows.First().UserId, DbSet.Where(x => x.UserId == userId));
        }

        public void AddOrIncrease(string userId, CartItem cartItem)
        {
            CartDbRow? cartDbRow = DbSet.Find(userId, cartItem.ItemId);

            if(cartDbRow == null)
            {
                DbSet.Add(new CartDbRow(userId, cartItem));
            }
            else
            {
                cartDbRow.Count += cartItem.Count;
            }

            Context.SaveChanges();
            /*Cart cart = Get(userId);
            CartItem dbCartItem = cart.CartItems.Find(x => x.ItemId == cartItem.ItemId);

            if(dbCartItem == null)
            {
                cart.CartItems.Add(cartItem);
            }
            else
            {
                dbCartItem.Count += cartItem.Count;
            }
            Context.SaveChanges();*/
        }

        public void DecreaseOrRemove(string userId, CartItem cartItem)
        {
            CartDbRow? cartDbRow = DbSet.Find(userId, cartItem.ItemId);

            if (cartDbRow != null)
            {
                if(cartDbRow.Count > cartItem.Count)
                {
                    cartDbRow.Count -= cartItem.Count;
                }
                else
                {
                    DbSet.Remove(cartDbRow);
                }
                Context.SaveChanges();
            }


            /*var cart = CreateIfNotExists(userId);
            CartItem dbCartItem = cart.CartItems.Find(x => x.ItemId == cartItem.ItemId);

            if (dbCartItem != null)
            {
                if(dbCartItem.Count > cartItem.Count)
                    dbCartItem.Count -= cartItem.Count;
                else
                    cart.CartItems.Remove(dbCartItem);
                Context.SaveChanges();
            }
            */
        }

    }
}

