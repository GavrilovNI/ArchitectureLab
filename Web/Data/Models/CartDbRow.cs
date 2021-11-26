﻿using System.ComponentModel.DataAnnotations;

namespace Web.Data.Models
{
    public class CartDbRow
    {
        [Key]
        [Required]
        public string UserId { get; set; }

        [Key]
        [Required]
        public long ItemId { get; set; }

        [Required]
        public int Count { get; set; }

        public CartDbRow(int count)
        {
            Count = count;
        }

        public CartDbRow(string userId, CartItem cartItem)
        {
            UserId = userId;
            ItemId = cartItem.ItemId;
            Count = cartItem.Count;
        }
    }
}
