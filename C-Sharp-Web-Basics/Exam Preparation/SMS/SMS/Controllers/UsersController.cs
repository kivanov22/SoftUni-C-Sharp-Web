namespace SMS.Controllers
{
    using SMS.Data;
    using SMS.Services;
    using static Data.DataConstants;
    public class UsersController
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly SMSDbContext data;

        public UsersController(
             IValidator validator,
             IPasswordHasher passwordHasher,
             SMSDbContext data)
        {
            this.validator = validator;
            this.data = data;
            this.passwordHasher = passwordHasher;
        }


    }
}
