using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Framework.Encryption;

namespace System
{
    /// <summary>
    /// Additional extensions on <see cref="System.String"/>
    /// </summary>
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        /// <summary>
        ///Returns the boolean check of whether a string can be considered as an email address.
        /// </summary>
        public static bool IsEmailAddress(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            var emailExpression = @"^(\w+([-+.']\w+)*)@([\w-]+\.)+[\w-]+$";
            return Regex.Match(value, emailExpression).Success;
        }

        /// <summary>
        /// Removes leading and training white-spaces from the System.String object.
        /// Safe to call on null or empty instances.
        /// </summary>
        public static string SafeTrim(this string input)
        {
            if (input.IsNotNullOrEmpty())
            {
                return input.Trim();
            }
            return input;
        }

        /// <summary>
        /// Limits this string to a given length and applies an ellipse
        /// </summary>
        public static string Ellipse(this string input, int length)
        {
            return input.Length > length ? input.Substring(0, length) + "..." : input;
        }

        /// <summary>
        /// Formats this string passing in the specified parameters
        /// </summary>
        public static string FormatString(this String format, params object[] values)
        {
            return string.Format(format, values);
        }

        /// <summary>
        /// Indicates whether this string is null or empty
        /// </summary>
        public static bool IsNullOrEmpty(this String value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Indicates whether this string is not null or empty
        /// </summary>
        public static bool IsNotNullOrEmpty(this String value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Strips spaces from string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSpaces(this string value)
        {
            return value.Replace(" ", string.Empty);
        }

        /// <summary>
        /// Strips spaces and special characters from the string.
        /// </summary>
        public static string RemoveWhiteSpaceAndSpecialChar(this string value)
        {
            return Regex.Replace(value, "[^a-zA-Z0-9]", string.Empty).RemoveSpaces();
        }

        /// <summary>
        /// Removes occurrences of characters, provided in the array
        /// </summary>
        public static string Remove(this string value, char[] exclusions)
        {
            if (value.IsNotNullOrEmpty())
            {
                var builder = new StringBuilder(value);

                var characterIndex = value.IndexOfAny(exclusions, 0);
                while (characterIndex != -1)
                {
                    builder.Remove(characterIndex, 1);
                    characterIndex = builder.ToString().IndexOfAny(exclusions, 0);
                }
                return builder.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Performs a case insensitive invariant culture comparison of two strings.
        /// </summary>
        public static bool IsSameAs(this string value, string target)
        {
            return string.Compare(value, target, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Converts the first letter of the first word to uppercase
        /// </summary>
        public static string FirstLetterToUppercase(this string value)
        {
            if (value.IsNotNullOrEmpty())
            {
                var convertedString = value.ToLowerInvariant();

                return convertedString[0].ToString().ToUpperInvariant() + convertedString.Substring(1);
            }
            return string.Empty;
        }

        /// <summary>
        /// Indicates whether this string is a number
        /// </summary>
        public static bool IsNumber(this String value)
        {
            int number;
            return int.TryParse(value, out number);
        }

        public static string ComputePBKDF2Hash(this string value, string salt, IPasswordEncryptor passwordEncryptor)
        {
            return passwordEncryptor.GenerateHash(value, salt);
        }
    }
}