using SharedTrip.Models.Trps;
using SharedTrip.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedTrip.Services
{
    using static Data.DataConstants;

    public class Validator : IValidator
    {
       

        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username ==null || user.Username.Length < UsernameMinLength || user.Username.Length >UsernameMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {UsernameMaxLength} characters long. ");
            }

            if (user.Email == null)
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > PasswordMaxLenght)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {PasswordMaxLenght} characters long.");
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

        public ICollection<string> ValidateTrip(AddTripFormModel trip)
        {
            var errors = new List<string>();

            if (trip.StartingPoint ==null)
            {
                errors.Add($"Starting point is required!");
            }

            if (trip.EndingPoint ==null)
            {
                errors.Add($"Ending point is required !");
            }

            if (trip.Seats == 0 || trip.Seats < SeatsMinLength || trip.Seats > SeatsMaxLength)
            {
                errors.Add($"The number of Seats must be between {SeatsMinLength} and {SeatsMaxLength}!");
            }

            if (trip.Description == null || trip.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"The description must not be more than {DescriptionMaxLength}.");
            }

            if (trip.Image == null || !Uri.IsWellFormedUriString(trip.Image, UriKind.Absolute))
            {
                errors.Add($"Image '{trip.Image}' is not valid. It must be a valid URL.");
            }

            return errors;
        }

    }
}
