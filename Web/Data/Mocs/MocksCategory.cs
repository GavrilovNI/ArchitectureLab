using Web.Data.Interfaces;
using Web.Data.Models;

namespace Web.Data.Mocs
{
    public class MocksCategory : ICarsCategory
    {
        public IEnumerable<Category> AllCategories
        {
            get
            {
                return new List<Category>()
                {
                    new Category() { Name = "Category1" },
                    new Category() { Name = "Category2" },
                };
            }
        }
    }
}
