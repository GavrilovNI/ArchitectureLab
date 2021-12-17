using System.ComponentModel.DataAnnotations;
using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class BoughtCart
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public List<BoughtProduct> Products { get; set; }

        public BoughtCart()
        {

        }

        public BoughtCart(Cart cart, IProductAccessor productAccessor)
        {
            UserId = cart.UserId;
            Time = DateTime.Now;
            Products = new List<BoughtProduct>();
            foreach(CartItem cartItem in cart.Items)
            {
                Product product = productAccessor.Get(cartItem.ItemId)!;
                BoughtProduct boughtProduct = new BoughtProduct(cartItem, product);
                Products.Add(boughtProduct);
            }
        }

        public void SetPaidStatusForAllProducts(PaidStatus paidStatus)
        {
            foreach (BoughtProduct product in Products)
            {
                product.PaidStatus = paidStatus;
            }
        }

    }
}
