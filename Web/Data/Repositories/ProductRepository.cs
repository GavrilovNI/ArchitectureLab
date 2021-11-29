using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repositories
{
    public class ProductRepository : Repository
    {
        public ProductRepository(DataContext context) : base(context)
        {

        }

        protected DbSet<Product> DbSet => Context.Products;

        public void Add(Product item)
        {
            DbSet.Add(item);
            Context.SaveChanges();
        }

        public Product? Get(long id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<Product> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public void Remove(long id)
        {
            Product? product = Get(id);
            if (product != null)
                DbSet.Remove(product);
            Context.SaveChanges();
        }
    }
}
