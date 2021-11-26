using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
    public class Cart
    {
        public string UserId { get; set; }

        public List<CartItem> CartItems { get; set; }

        public Cart(string userId, IEnumerable<CartDbRow> cartDbRows)
        {
            UserId = userId;
            CartItems = cartDbRows.Select(x => new CartItem(x.ItemId, x.Count)).ToList();

        }
    }
}
