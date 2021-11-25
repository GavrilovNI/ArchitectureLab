﻿using System.ComponentModel.DataAnnotations;


namespace Arhitecture.Data.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Price Price { get; set; }

        [Required]
        public string Description { get; set; }

        public Product(string Name, Price price, string description)
        {
            throw new NotImplementedException();
        }
    }
}
