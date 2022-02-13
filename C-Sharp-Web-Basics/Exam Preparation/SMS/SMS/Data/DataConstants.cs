namespace SMS.Data
{
    public class DataConstants
    {
        //User
        public const int UsernameMaxLength = 20;
        public const int UsernameMinLength = 5;
        public const string ValidEmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public const int PasswordMaxLength = 20;
        public const int PasswordMinLength = 5;

        //Product
        public const int ProductNameMinLenght = 4;
        public const int ProductNameMaxLength = 20;

        public const decimal ProductPriceMinLength = 0.05M;
        public const decimal ProductPriceMaxLength = 1000M;
    }
}
