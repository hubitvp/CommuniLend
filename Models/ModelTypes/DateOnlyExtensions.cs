using System.Globalization;

namespace CommuniLend.Models.ModelTypes
{



    public static class DateOnlyExtensions
    {
        public const string DefaultDateFormat = "dd MMMM yyyy";

        public static string ToFormattedString(
            this DateOnly date,
            string dateFormat = DefaultDateFormat,
            IFormatProvider? formatProvider = null)
        {
            return date.ToString(dateFormat, formatProvider ?? CultureInfo.InvariantCulture);
        }
    }
}
