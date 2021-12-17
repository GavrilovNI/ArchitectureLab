using Web.Data.Models;

namespace Web.Data.Repositories
{
    public interface IProductAccessor
    {
        Product? Get(long id);
    }
}
