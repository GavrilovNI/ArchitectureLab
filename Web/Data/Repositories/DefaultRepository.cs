using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Repositories
{
    public abstract class DefaultRepository<T> : Repository where T : class
    {
        public DefaultRepository(DataContext context) : base(context)
        {
        }

        protected abstract DbSet<T> DbSet { get; }

        public abstract void Add(T item);

        public abstract T? Get(long id);

        public abstract Dictionary<long, T> GetAllAsDictionary();

        public abstract List<T> GetAllAsList();

        public abstract void Remove(long id);

        public abstract void Update(T item);
    }
}
