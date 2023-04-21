namespace Elwin.GoGroceries.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str)) throw new ArgumentNullException(nameof(str));
            return char.ToUpper(str[0]) + str[1..];
        }
    }
}
