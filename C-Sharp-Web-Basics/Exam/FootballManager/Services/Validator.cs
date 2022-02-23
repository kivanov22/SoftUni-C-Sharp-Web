using FootballManager.ViewModels.Players;
using FootballManager.ViewModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace FootballManager.Services
{

    using static Data.DataConstants;
    public class Validator : IValidator
    {
        public ICollection<string> ValidatePlayer(PlayerFormModel player)
        {
            var errors = new List<string>();

            if (player.FullName == null || player.FullName.Length < FullNameMinLength || player.FullName.Length > FullNameMaxLength)
            {
                errors.Add($"Fullname '{player.FullName}' is not valid. It must be between {FullNameMinLength} and {FullNameMaxLength} characters long. ");
            }

            //if (player.ImageUrl == null)
            //{
            //    errors.Add($"ImageUrl '{player.ImageUrl}' is not valid.");
            //}

            if (player.Position == null || player.Position.Length < PositionMinLength || player.Position.Length > PositionMaxLength)
            {
                errors.Add($"Position '{player.Position}' is not valid. It must be between {PositionMinLength} and {PositionMaxLength} characters long. ");
            }

            if (player.Speed < 0 || player.Speed>10)
            {
                errors.Add("Speed cannot be negative or bigged than 10.");
            }


            if (player.Endurance < 0 || player.Endurance >10)
            {
                errors.Add("Endurance cannot be negative or bigger than 10.");
            }

            if (player.Description==null || player.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"Description '{player.Description}' is not valid. Cannot be longer than {DescriptionMaxLength} characters.");

            }
            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > UsernameMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long. ");
            }

            if (user.Email == null || user.Email.Length < EmailMinLength || user.Email.Length> EmailMaxLength)
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
    }
}
