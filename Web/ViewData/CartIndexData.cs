using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using Web.Data.Models;

namespace Web.ViewData
{
    public class CartIndexData
    {
        public IEnumerable<ProductInfo> Products { get; set; }

        [DisplayName("Delivery address")]
        public string DeliveryAddress { get; set; }

        public CartIndexData(IEnumerable<ProductInfo> products)
        {
            Products = products;
            DeliveryAddress = "";
        }

        public CartIndexData()
        {
        }

    }
}
