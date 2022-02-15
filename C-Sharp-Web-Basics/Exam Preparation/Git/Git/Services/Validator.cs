using Git.ModelViews.Commit;
using Git.ModelViews.Repository;
using Git.ModelViews.User;

namespace Git.Services
{

    using static Data.DataConstants;
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long. ");
            }

            if (user.Email == null)
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > PasswordMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            return errors;
        }
        public ICollection<string> ValidateRepository(CreateRepositoryFormModel repository)
        {
            var errors = new List<string>();

            if (repository.Name == null || repository.Name.Length < RepositoryNameMinLength || repository.Name.Length > RepositoryNameMaxLength)
            {
                errors.Add($"Username '{repository.Name}' is not valid. It must be between {RepositoryNameMinLength} and {RepositoryNameMaxLength} characters long. ");
            }

            return errors;
        }

        public ICollection<string> ValidateCommit(CreateCommitFormModel commit)
        {
            var errors = new List<string>();

            if (commit.Description == null || commit.Description.Length< DescriptionMinLength)
            {
                errors.Add($"Commit '{commit.Description}' is not valid. It must be minimum {DescriptionMinLength} characters long. ");
            }

            return errors;
        }
    }
}
