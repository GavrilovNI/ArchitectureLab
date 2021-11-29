using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class ProductInfo
    {
        public Product? Product { get; private set; }
        public int CountInCart { get; private set; }

        public ProductInfo(ProductRepository productRepository, CartItem cartItem)
        {
            Product = productRepository.Get(cartItem.ItemId);
            CountInCart = cartItem.Count;
        }
        public ProductInfo(Product product, int countInCart = 0)
        {
            Product = product;
            CountInCart = countInCart;
        }
    }
}
