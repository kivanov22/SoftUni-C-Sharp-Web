namespace Git.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Commit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MinLength(DescriptionMinLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public string RepositoryId { get; set; }

        public Repository Repository { get; set; }
    }
}
