using Arhitecture.Data.Models;

namespace Arhitecture.Data.Repositories
{
    public class UserRepository : DefaultRepository<User>
    {
        public UserRepository(DataContext context) : base(context)
        {
            throw new NotImplementedException();
        }
    }
}
