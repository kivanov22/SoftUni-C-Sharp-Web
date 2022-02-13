using SMS.Models.Products;
using SMS.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services
{
    using static Data.DataConstants;
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();
            //Username
            //Email
            //Password
            if (user.Username == null || user.Username.Length<UsernameMinLength || user.Username.Length>UsernameMaxLength)
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
        public ICollection<string> ValidateProducts(CreateProductFormModel product)
        {
            var errors = new List<string>();
            //Name
            //Price
            if (product.Name ==null || product.Name.Length < ProductNameMinLenght || product.Name.Length > ProductNameMaxLength)
            {
                errors.Add($"Username '{product.Name}' is not valid. It must be between {ProductNameMinLenght} and {ProductNameMaxLength} characters long. ");
            }

            if (product.Price < ProductPriceMinLength || product.Price > ProductPriceMaxLength)
            {
                errors.Add($"Product '{product.Price}' is not valid. It must be between {ProductPriceMinLength} and {ProductPriceMaxLength}");
            }

            return errors;
        }

    }
}
