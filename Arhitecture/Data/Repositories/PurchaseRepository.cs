using Arhitecture.Data.Models;

namespace Arhitecture.Data.Repositories
{
    public class PurchaseRepository : DefaultRepository<Purchase>
    {
        public PurchaseRepository(DataContext context) : base(context)
        {
            throw new NotImplementedException();
        }
    }
}
