using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Web.Data.Models
{
    public class Product
    {
        [Key]
        [Required]
        public long Id { get; set; }

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

        public Product(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            AvaliableAmount = product.AvaliableAmount;
            LinkToImage = product.LinkToImage;
        }

        public Product()
        {
        }

        public static bool operator ==(Product a, Product b)
        {
            if (a is null)
            {
                if (b is null)
                {
                    return true;
                }

                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Product a, Product b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            Product product = (Product)obj;

            return Id == product.Id &&
                Name == product.Name &&
                Price == product.Price &&
                Description == product.Description &&
                AvaliableAmount == product.AvaliableAmount &&
                LinkToImage == product.LinkToImage;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Price, Description, AvaliableAmount, LinkToImage);
        }

        public override string ToString()
        {
            return "{" +
                nameof(Id) + ": " + Id + ", " +
                nameof(Price) + ": " + Price + ", " +
                nameof(Description) + ": '" + Description + "', " +
                nameof(AvaliableAmount) + ": " + AvaliableAmount + ", " +
                nameof(LinkToImage) + ": '" + LinkToImage + "'" +
                "}";
        }
    }
}
