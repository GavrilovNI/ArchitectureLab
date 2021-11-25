using System.ComponentModel.DataAnnotations;


namespace Arhitecture.Data.Models
{
    public class Catalog
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<Product> Products { get; set; }

        public Catalog(string name, string description, List<Product> products)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
