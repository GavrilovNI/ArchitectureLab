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

        public void Add(BoughtCart item)
        {
            DbSet.Add(item);
            Context.SaveChanges();
        }

        public BoughtCart? Get(long id)
        {
            return DbSet.Find(id);
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
