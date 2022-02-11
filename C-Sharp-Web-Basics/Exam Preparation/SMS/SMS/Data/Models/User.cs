namespace SMS.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants;
    public class User
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get;set; }

        [Required]
        [RegularExpression(ValidEmailRegex)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string CartId { get; init; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
    }
}
