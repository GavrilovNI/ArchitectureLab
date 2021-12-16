using System.Linq.Expressions;
using Web.Data.Models;

namespace Web.Data.Enum
{
    using KeySelector = Expression<Func<Product, object>>;

    public class ProductSorter
    {

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
