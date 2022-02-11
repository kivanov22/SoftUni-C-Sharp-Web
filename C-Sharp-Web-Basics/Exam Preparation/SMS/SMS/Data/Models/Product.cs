namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants;
    public class Product
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Name { get; set; }

        [Range(0.05,1000)]
        public decimal Price { get; set; }

    
        public string CartId { get; init; }

        [ForeignKey(nameof(CartId))]

        public Cart Cart { get; set; }
    }
}
