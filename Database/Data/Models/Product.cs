using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Web.Data.Models
{
    public class Product
    {
        [Key]
        [Required]
        public long Id { get; private set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Price")]
        public float Price { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Avaliable amount")]
        public int AvaliableAmount { get; set; }

        [Required]
        [DisplayName("Link To Image")]
        public string LinkToImage { get; set; }

        public Product(string name, float price, string description, int avaliableAmount, string linkToImage)
        {
            Name = name;
            Price = price;
            Description = description;
            AvaliableAmount = avaliableAmount;
            LinkToImage = linkToImage;
        }

        public Product()
        {
        }
    }
}
