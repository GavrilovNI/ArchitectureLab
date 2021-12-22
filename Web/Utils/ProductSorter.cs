using System.Linq.Expressions;
using Web.Data.Models;

namespace Web.Utils
{
    using KeySelector = Expression<Func<Product, object>>;

    public class ProductSorter
    {
        public const string DEFAULT_SORTING_KEY = "name";
        public string? SortingKey { get; set; } = null;

        public ProductSorter()
        {
        }

        public IQueryable<Product> Apply(IQueryable<Product> products)
        {
            string key = SortingKey ?? DEFAULT_SORTING_KEY;
            if (_keySelectors.ContainsKey(key) == false)
                key = DEFAULT_SORTING_KEY;
            KeySelector? keySelector = GetKeySelector(key);
            if (keySelector != null)
                products = products.OrderBy(keySelector);
            return products;
        }


        private static Dictionary<string, KeySelector> _keySelectors = new Dictionary<string, KeySelector>();

        static ProductSorter()
        {
            RegisterKeySelector("Name", x => x.Name);
            RegisterKeySelector("Price", x => x.Price);
            RegisterKeySelector("Count", x => x.AvaliableAmount);
        }

        public static void RegisterKeySelector(string name, KeySelector keySelector)
        {
            _keySelectors.Add(name.ToLower(), keySelector);
        }

        public static KeySelector? GetKeySelector(string? name)
        {
            if (name == null)
                return null;
            return _keySelectors!.GetValueOrDefault(name.ToLower(), null);
        }
    }
}
