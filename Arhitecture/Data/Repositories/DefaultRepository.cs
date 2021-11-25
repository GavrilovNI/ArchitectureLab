using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhitecture.Data.Repositories
{
    public abstract class DefaultRepository<T> : Repository
    {
        public DefaultRepository(DataContext context) : base(context)
        {
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public T? Get(long id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<long, T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
