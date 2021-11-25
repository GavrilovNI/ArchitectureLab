

namespace Web.Data.Repositories
{
    public class Repository
    {
        protected DataContext Context;

        protected Repository(DataContext context)
        {
            Context = context;
        }
    }
}
