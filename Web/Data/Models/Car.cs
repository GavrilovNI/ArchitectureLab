namespace Web.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
    }
}
