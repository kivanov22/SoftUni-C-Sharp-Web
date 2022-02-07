namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Product
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Name { get; set; }

        [Range(0.05,1000)]
        public decimal Price { get; set; }

        public Cart Cart { get; init; }
    }
}
