using Web.Data.Models;

namespace Web.Utils
{
    public class ProductFilter
    {
        public int? MinPrice { get; set; } = null;
        public int? MaxPrice { get; set; } = null;

        public int? AvaliableAmountMin { get; set; } = null;

        public ProductFilter()
        {

        }

        public IQueryable<Product> Apply(IQueryable<Product> products)
        {
            IQueryable<Product> result = products;
            if (MinPrice != null)
                result = result.Where(x => x.Price >= MinPrice);
            if (MaxPrice != null)
                result = result.Where(x => x.Price <= MaxPrice);
            if (AvaliableAmountMin != null)
                result = result.Where(x => x.AvaliableAmount >= AvaliableAmountMin);
            return result;
        }
    }
}
