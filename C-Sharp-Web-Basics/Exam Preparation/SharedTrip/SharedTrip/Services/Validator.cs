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

            if (user.Username ==null || user.Username.Length < UsernameMinLength || user.Username.Length >DefaultMaxLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {DefaultMaxLength} characters long. ");
            }

            if (user.Email == null)
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {DefaultMaxLength} characters long.");
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

            if (trip.StartPoint ==null)
            {
                errors.Add($"Starting point is required!");
            }

            if (trip.EndPoint ==null)
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

            if (trip.ImagePath == null || !Uri.IsWellFormedUriString(trip.ImagePath, UriKind.Absolute))
            {
                errors.Add($"Image '{trip.ImagePath}' is not valid. It must be a valid URL.");
            }

            return errors;
        }

    }
}
