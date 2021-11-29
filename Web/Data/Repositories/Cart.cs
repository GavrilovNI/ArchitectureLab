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

        public IQueryable<CartItem> GetAll()
        {
            return Context.Carts.Where(x => x.UserId == UserId).Select(x => new CartItem(Context, UserId, x.ItemId));
        }
    }
}
