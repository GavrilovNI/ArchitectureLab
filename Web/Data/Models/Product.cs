using System.ComponentModel.DataAnnotations;


namespace Web.Data.Models
{
    public class Product
    {
        [Key]
        [Required]
        public long Id { get; private set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int AvaliableAmount { get; set; }

        public string LinkToImage { get; set; }

        public Product(string name, float price, string description, int avaliableAmount, string linkToImage)
        {
            Name = name;
            Price = price;
            Description = description;
            AvaliableAmount = avaliableAmount;
            LinkToImage = linkToImage;
        }

        public void Update(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            AvaliableAmount = product.AvaliableAmount;
            LinkToImage = product.LinkToImage;
        }
    }
}
