using Microsoft.EntityFrameworkCore;
using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class ProductBill
    {
        public List<BoughtProduct> boughtProducts { get; set; }

        public string DeliveryAddress { get; set; }

        public ProductBill(long productId, BoughtCartRepository boughtCartRepository)
        {
            List<BoughtCart> allCarts = boughtCartRepository.GetAll()
                                                            .Include(x => x.BoughtProducts)
                                                            .ToList();
            IEnumerable<BoughtCart> neededCarts = allCarts
                                                  .Where(x => x.BoughtProducts
                                                               .Any(x => x.ProductId == productId)
                                                        );

            List<BoughtProduct> boughtProducts = neededCarts
                                                 .Select(x => x.BoughtProducts
                                                               .Find(x => x.ProductId == productId)
                                                        )
                                                 .ToList();



        }
    }
}
