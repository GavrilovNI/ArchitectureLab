using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [InverseProperty("BoughtCart")]
        public List<BoughtProduct> BoughtProducts { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        public BoughtCart()
        {

        }

        public BoughtCart(Cart cart, IProductAccessor productAccessor, string deliveryAddress)
        {
            UserId = cart.UserId;
            Time = DateTime.Now;
            BoughtProducts = new List<BoughtProduct>();
            foreach(CartItem cartItem in cart.Items)
            {
                Product product = productAccessor.Get(cartItem.ItemId)!;
                BoughtProduct boughtProduct = new BoughtProduct(cartItem, product, this);
                BoughtProducts.Add(boughtProduct);
            }
            DeliveryAddress = deliveryAddress;
        }

        public void SetPaidStatusForAllProducts(PaidStatus paidStatus)
        {
            foreach (BoughtProduct product in BoughtProducts)
            {
                product.PaidStatus = paidStatus;
            }
        }

    }
}
