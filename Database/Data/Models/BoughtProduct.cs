using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        [Required]
        public long BoughtCartId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BoughtCartId))]
        public BoughtCart BoughtCart { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [JsonIgnore]
        public float TotalPrice => Count * Price;

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
