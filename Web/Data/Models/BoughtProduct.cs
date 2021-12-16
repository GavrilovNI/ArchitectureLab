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
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public PaidStatus Paid { get; set; }

        public BoughtProduct()
        {

        }

        public BoughtProduct(CartItem cartItem, float price)
        {
            UserId = cartItem.UserId;
            ProductId = cartItem.ItemId;
            Count = cartItem.Count;
            Time = DateTime.Now;
            Price = price;
            Paid = PaidStatus.NotPaid;
        }
    }
}
