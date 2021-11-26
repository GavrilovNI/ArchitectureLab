using System.ComponentModel.DataAnnotations;

namespace Web.Data.Models
{
    public class CartItem
    {
        public long ItemId { get; set; }

        public int Count { get; set; }

        public CartItem(long itemId, int count)
        {
            ItemId = itemId;
            Count = count;
        }
    }
}
