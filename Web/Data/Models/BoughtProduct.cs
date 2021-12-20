using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
    public enum PaidStatus
    {
        NotPaid,
        Paid,
        Cancelled
    }

    public class BoughtProduct
    {
        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public PaidStatus PaidStatus { get; set; }

        [Required]
        public long BoughtCartId { get; set; }
        
        [ForeignKey(nameof(BoughtCartId))]
        public BoughtCart BoughtCart { get; set; }

        public BoughtProduct()
        {

        }

        public BoughtProduct(CartItem cartItem, Product product, BoughtCart boughtCart)
        {
            ProductId = cartItem.ItemId;
            Count = cartItem.Count;
            Price = product.Price;
            PaidStatus = PaidStatus.NotPaid;
            BoughtCart = boughtCart;
        }
    }
}
