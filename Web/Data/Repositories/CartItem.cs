using System.ComponentModel.DataAnnotations;
using Web.Data.Repositories;

namespace Web.Data.Models
{

    public class CartItem : Repository
    {
        public string UserId { get; set; }
        public long ItemId { get; set; }

        private CartDbRow? Row => Context.Carts.Find(UserId, ItemId);

        public int Count
        {
            get
            {
                return Row?.Count ?? 0;
            }
            set
            {
                CartDbRow? cartDbRow = Row;
                if(cartDbRow != null)
                {
                    if (value <= 0)
                        Context.Carts.Remove(cartDbRow);
                    else
                        cartDbRow.Count = value;
                }
                else if(value > 0)
                {
                    Context.Carts.Add(new CartDbRow(UserId, ItemId, value));
                }
                Context.SaveChanges();
            }
        }

        public CartItem(DataContext context, string userId, long itemId) : base(context)
        {
            UserId = userId;
            ItemId = itemId;
        }
    }
}
