namespace Git.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        //[MaxLength(PasswordMaxLength)]
        public string Password { get; set; }

        public ICollection<Repository> Repositories { get; set; } = new List<Repository>();

        public ICollection<Commit> Commits { get; set; } = new List<Commit>();
    }
}
