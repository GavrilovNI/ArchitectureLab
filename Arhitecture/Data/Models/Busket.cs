using System.ComponentModel.DataAnnotations;


namespace Arhitecture.Data.Models
{
    public class Busket
    {
        [Key]
        [Required]
        public long UserId { get; set; }

        [Required]
        public Dictionary<Product, int> Products { get; set; }

        public Busket()
        {
            throw new NotImplementedException();
        }
        public Busket(Dictionary<Product, int> products)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product, int amount = 1)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
