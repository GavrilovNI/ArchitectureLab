using Arhitecture.Data.Models;

namespace Arhitecture.Data.Repositories
{
    public class CatalogRepository : DefaultRepository<Catalog>
    {
        public CatalogRepository(DataContext context) : base(context)
        {
            
        }
    }
}
