namespace SMS.Services
{
    using SMS.Models.Products;
    using SMS.Models.Users;
    using System.Collections.Generic;
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel);

        ICollection<string> ValidateProducts(CreateProductFormModel);
    }
}
