using Microsoft.AspNetCore.Mvc;
using Web.Data.Models;

namespace Web.Data.Utils
{
    public class ProductSearcher
    {
        [FromQuery(Name = "search")]
        public string? SearchingString { get; set; } = null;

        public IQueryable<Product> Apply(IQueryable<Product> products)
        {
            if(SearchingString == null)
                return products;
            string[] strings = SearchingString.Split(' ');
            if(strings.Length == 0)
                return products;
            IQueryable<Product> result = products;
            return products.ToList().Where(p => {
                foreach(string str in strings)
                {
                    string[] stringsToSearchIn = new string[]
                    {
                        p.Price.ToString(),
                        p.Name,
                        p.Description,
                        p.AvaliableAmount.ToString(),
                    };
                    foreach(string stringToSearchIn in stringsToSearchIn)
                    {
                        if(stringToSearchIn.Contains(str))
                            return true;
                    }
                }
                return false;
            }).AsQueryable();
        }
    }
}
