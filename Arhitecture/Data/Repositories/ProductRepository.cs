using Arhitecture.Data.Models;

namespace Arhitecture.Data.Repositories
{
    public class ProductRepository : DefaultRepository<Product>
    {
        public ProductRepository(DataContext context) : base(context)
        {
            throw new NotImplementedException();
        }
    }
}
