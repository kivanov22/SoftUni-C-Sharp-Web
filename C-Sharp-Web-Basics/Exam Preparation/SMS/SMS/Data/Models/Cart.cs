namespace SMS.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Cart
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public User User { get; init; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}
