using Web.Data.Models;

namespace Web.Data.Utils
{
    public class PageSelector
    {
        public int PageSize { get; set; } = 10;
        public int? Page { get; set; } = null;


        public IQueryable<Product> Apply(IQueryable<Product> products)
        {
            if(Page == null)
                return products;
            int startIndex = Page.Value * PageSize;
            return products.Skip(startIndex).Take(PageSize);
        }
    }
}
