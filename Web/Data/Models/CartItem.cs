using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Data.Repositories;

namespace Web.Data.Models
{

    public class CartItem
    {
        [Required]
        public long ItemId { get; set; }

        [Required]
        public string CartId { get; set; }

        [Required]
        public int Count { get; set; }

        public bool IsEmpty => Count <= 0;

        public CartItem()
        {

        }

        public CartItem(long itemId, string cartId, int count = 0)
        {
            ItemId = itemId;
            CartId = cartId;
            Count = count;
        }
    }
}
