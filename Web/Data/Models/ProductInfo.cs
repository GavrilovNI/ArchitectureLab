using System.Text.Json.Serialization;
using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class ProductInfo
    {
        public Product? Product { get; private set; }
        public int CountInCart { get; private set; }

        [JsonIgnore]
        public float TotalPrice => CountInCart * (Product?.Price ?? 0);

        public ProductInfo(ProductRepository productRepository, CartItem cartItem)
        {
            Product = productRepository.GetCopy(cartItem.ItemId);
            CountInCart = cartItem.Count;
        }
        public ProductInfo(Product product, int countInCart = 0)
        {
            Product = product;
            CountInCart = countInCart;
        }
    }
}
