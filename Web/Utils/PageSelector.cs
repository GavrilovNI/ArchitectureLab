using Web.Data.Models;

namespace Web.Data.Utils
{
    public class PageSelector<T>
    {
        public int PageSize { get; set; } = 10;
        public int? Page { get; set; } = null;


        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if(Page == null)
                return query;
            int startIndex = Page.Value * PageSize;
            return query.Skip(startIndex).Take(PageSize);
        }
    }
}
