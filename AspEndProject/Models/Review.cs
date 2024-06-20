﻿namespace AspEndProject.Models
{
    public class Review : BaseEntity
    {
        public string? Message { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
