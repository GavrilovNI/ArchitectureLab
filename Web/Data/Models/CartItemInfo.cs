using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class CartItemInfo
    {
        private Product _product;
        public string Name => _product.Name;
        public int Count { get; private set; }

        public CartItemInfo(CartItem cartItem, ProductRepository productRepository)
        {
            _product = productRepository.Get(cartItem.ItemId);
            Count = cartItem.Count;
        }
    }
}
