using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

namespace Web.Data.Repositories
{
    public class ProductRepository : Repository, IProductAccessor
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

        public Product? GetCopy(long id)
        {
            Product? product = Get(id);
            return product == null ? null : new Product(product);
        }

        public IQueryable<Product> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public void Remove(long id)
        {
            Product? product = Get(id);
            if (product != null)
            {
                DbSet.Remove(product);
                Context.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            Product? dbProduct = Get(product.Id);
            if (dbProduct == null)
                return;
            if (dbProduct == product)
                return;
            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Description = product.Description;
            dbProduct.AvaliableAmount = product.AvaliableAmount;
            dbProduct.LinkToImage = product.LinkToImage;
            Context.SaveChanges();
        }
    }
}
