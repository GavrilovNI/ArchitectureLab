using Web.Data.Models;

namespace Web.Data.Repositories
{
    public interface IProductAccessor
    {
        Product? GetCopy(long id);
    }
}
