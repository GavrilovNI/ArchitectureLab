using System.ComponentModel.DataAnnotations;


namespace Arhitecture.Data.Models
{
    public class Purchase
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public PurchaseStatus Status { get; set; }

        [Required]
        public Dictionary<Product, int> Products { get; set; }

        public Purchase(long userId, PurchaseStatus status, Dictionary<Product, int> products)
        {
            throw new NotImplementedException();
        }
    }
}
