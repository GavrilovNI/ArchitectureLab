using System.ComponentModel.DataAnnotations;

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
        [Key]
        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public PaidStatus PaidStatus { get; set; }

        public BoughtProduct()
        {

        }

        public BoughtProduct(CartItem cartItem, Product product)
        {
            ProductId = cartItem.ItemId;
            Count = cartItem.Count;
            Price = product.Price;
            PaidStatus = PaidStatus.NotPaid;
        }
    }
}
