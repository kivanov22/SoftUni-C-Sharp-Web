namespace CarShop.Data
{
    public class DataConstants
    {
        public const int UsernameMaxLength = 20;
        public const int UsernameMinLength = 4;
        public const int PasswordMinLength = 5;
        public const int PasswordMaxLength = 20;
        public const string UserTypeClient = "Client";
        public const string UserTypeMechanic = "Mechanic";

        public const int CarModelMaxLength = 20;
        public const int CarModelMinLength = 5;
        public const string PlateNumberRegex = @"[A-Z]{2}[0-9]{4}[A-Z]{2}";

        public const int DescriptionMinLength = 5;

    }
}
