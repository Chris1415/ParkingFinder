using System.Text;

namespace Hach.Library.Extensions
{
    /// <summary>
    /// String Extensions
    /// </summary>
    /// <author>
    /// Christian Hahn, Jun-2016
    /// </author>
    public static class StringExtension
    {
        /// <summary>
        /// Is Null or Empty Helper
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>true if the input string is null or empty</returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return input == null || input.Equals(string.Empty);
        }

        /// <summary>
        /// Encodes a given strin to UTF 8
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>UTF 8 encoded string</returns>
        public static string ToUtf8(this string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
