using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repositories
{
    public class BoughtCartRepository : Repository
    {
        public BoughtCartRepository(DataContext context) : base(context)
        {
        }

        protected DbSet<BoughtCart> DbSet => Context.BoughtCarts;

        public BoughtCart Add(BoughtCart item)
        {
            var entityEntry = DbSet.Add(item);
            Context.SaveChanges();
            return entityEntry.Entity;
        }

        public BoughtCart? Get(long id)
        {
            return GetAll()
                   .Where(x => x.Id == id)
                   .Include(x => x.BoughtProducts)
                   .FirstOrDefault(x => true, null);
        }

        public IQueryable<BoughtCart> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public void Update(BoughtCart boughtCart)
        {
            DbSet.Update(boughtCart);
            Context.SaveChanges();
        }
    }
}
