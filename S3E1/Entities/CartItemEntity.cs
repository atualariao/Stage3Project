﻿using System.ComponentModel.DataAnnotations;

namespace S3E1.Entities
{
    public class CartItemEntity
    {
        [Key]
        public Guid ItemID { get; set; }
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemStatus { get; set; } = "Pending";
    }
}
