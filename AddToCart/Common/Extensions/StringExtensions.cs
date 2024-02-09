using System.Text.RegularExpressions;

namespace AddToCart.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetNumericPart(this string input)
        {
            Match match = Regex.Match(input, @"\d+(\.\d+)?");
            string numericPart = "";
            if (match.Success)
            {
                numericPart = match.Value;
                Console.WriteLine($"Extracted Numeric Part: {numericPart}");
            }
            else
            {
                Console.WriteLine("No numeric part found in the string.");
            }

            return numericPart;
        }
    }
}