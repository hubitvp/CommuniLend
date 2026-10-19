namespace CommuniLend.Data.Models.Constraints
{
    public static class ValidationConstants
    {


        public static class User
        {
            public const int FullNameMaxLength = 100;
            public const int EmailMaxLength = 254;
            public const int PhoneNumberMaxLength = 15;
            public const int AddressMaxLength = 200;

            public const int ApplicationUserIdMaxLength = 450;
        }


        public static class Tool
        {

            public const int NameMinLength = 3;

            public const int NameMaxLength = 100;

            public const int DescriptionMinLength = 3;

            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMaxLength = 512;

            public const int PictureMaxFileSize = 50 * 1024;

        }

        public static class Category
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 500;

        }

        public static class Loan
        {
            public const int NotesMaxLength = 1000;
        }
    }
}
