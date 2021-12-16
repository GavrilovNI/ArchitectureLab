using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repositories
{
    public class BoughtProductRepository : Repository
    {
        public BoughtProductRepository(DataContext context) : base(context)
        {
        }

        protected DbSet<BoughtProduct> DbSet => Context.BoughtProducts;

        public void Add(BoughtProduct item)
        {
            DbSet.Add(item);
            Context.SaveChanges();
        }

        public BoughtProduct? Get(long id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<BoughtProduct> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public void Update(BoughtProduct product)
        {
            DbSet.Update(product);
            Context.SaveChanges();
        }
    }
}
