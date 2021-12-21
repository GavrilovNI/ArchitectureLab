using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class Cart
    {
        public string UserId { get; private set; }

        private List<CartItem> _items;
        public IEnumerable<CartItem> Items => _items;

        public Cart(string userId)
        {
            UserId = userId;
            _items = new List<CartItem>();
        }

        public Cart(string userId, List<CartItem> items)
        {
            UserId = userId;
            _items = items;
        }

        public void AddItems(List<CartItem> items)
        {
            _items.AddRange(items);
        }

        public CartItem GetItemOrCreate(long id)
        {
            CartItem? cartItem = GetItem(id);
            if (cartItem == null)
            {
                cartItem = new CartItem(id, UserId);
                _items.Add(cartItem);
            }
            return cartItem;
        }
        public CartItem? GetItem(long id)
        {
            return _items.FirstOrDefault(x => x.ItemId == id, null);
        }

        public bool CanBeApplied(IProductAccessor productAccessor)
        {
            foreach(CartItem cartItem in _items)
            {
                Product? product = productAccessor.Get(cartItem.ItemId);
                if(product == null)
                    return false;
                if (product.AvaliableAmount < cartItem.Count)
                    return false;
            }
            return true;
        }

        public void Fix(IProductAccessor productAccessor)
        {
            foreach (CartItem cartItem in _items)
            {
                Product? product = productAccessor.Get(cartItem.ItemId);
                if (product == null)
                    cartItem.Count = 0;
                if (product.AvaliableAmount < cartItem.Count)
                    cartItem.Count = product.AvaliableAmount;
            }
            RemoveEmptyItems();
        }

        public void RemoveEmptyItems()
        {
            _items.RemoveAll(x => x.Count <= 0);
        }
        public void RemoveIfEmpty(long itemId)
        {
            CartItem? cartItem = GetItem(itemId);
            if(cartItem != null && cartItem.IsEmpty)
                _items.Remove(cartItem);
        }

        public void Apply(BoughtCartRepository boughtCartRepository, ProductRepository productRepository, string deliveryAddress)
        {
            BoughtCart boughtCart = new BoughtCart(this, productRepository, deliveryAddress);

            foreach(BoughtProduct boughtProduct in boughtCart.BoughtProducts)
            {
                Product product = productRepository.Get(boughtProduct.ProductId)!;
                product.AvaliableAmount -= boughtProduct.Count;
                productRepository.Update(product);
            }

            boughtCartRepository.Add(boughtCart);
        }
    }
}
