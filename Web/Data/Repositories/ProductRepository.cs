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

        public Dictionary<long, Product> GetAllAsDictionary()
        {
            return DbSet.ToDictionary(c => c.Id);
        }

        public List<Product> GetAllAsList()
        {
            return DbSet.ToList();
        }

        public void Remove(long id)
        {
            Product? product = Get(id);
            if (product != null)
                DbSet.Remove(product);
            Context.SaveChanges();
        }

        public void Update(Product item)
        {
            Product? toUpdate = DbSet.Find(item.Id);
            if(toUpdate == null)
            {
                Add(item);
                return;
            }
            toUpdate.Name = item.Name;
            toUpdate.Price = item.Price;
            toUpdate.Description = item.Description;
            toUpdate.AvaliableAmount = item.AvaliableAmount;
            toUpdate.LinkToImage = item.LinkToImage;
            Context.SaveChanges();
        }
    }
}
