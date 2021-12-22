using Microsoft.EntityFrameworkCore;
using Web.Data.Repositories;

namespace Web.Data.Models
{
    public class ProductBill
    {
        public BoughtProduct BoughtProduct { get; set; }
        public string DeliveryAddress { get; set; }

        public ProductBill(BoughtProduct boughtProduct, string deliveryAddress)
        {
            BoughtProduct = boughtProduct;
            DeliveryAddress = deliveryAddress;
        }

        public static List<ProductBill> Create(long productId, BoughtCartRepository boughtCartRepository)
        {
            List<BoughtCart> allCarts = boughtCartRepository.GetAll()
                                                            .Include(x => x.BoughtProducts)
                                                            .ToList();

            IEnumerable<BoughtCart> neededCarts = allCarts.Where(x => x.BoughtProducts.Any(x => x.ProductId == productId));

            List<ProductBill> productBills = neededCarts.Select(x =>
                                                                    new ProductBill(
                                                                        x.BoughtProducts.Find(x => x.ProductId == productId)!,
                                                                        x.DeliveryAddress)
                                                                ).ToList();

            return productBills;
        }
    }
}
