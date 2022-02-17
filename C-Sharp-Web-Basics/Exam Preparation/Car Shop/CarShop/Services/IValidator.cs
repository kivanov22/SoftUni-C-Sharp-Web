using CarShop.ModelViews.Cars;
using CarShop.ModelViews.Issues;
using CarShop.ModelViews.Users;
using System.Collections.Generic;

namespace CarShop.Services
{
    public interface IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel model);

        public ICollection<string> ValidateCar(AddCarFormModel model);

        public ICollection<string> ValidateIssue(AddIssueFormModel model);
    }
}
