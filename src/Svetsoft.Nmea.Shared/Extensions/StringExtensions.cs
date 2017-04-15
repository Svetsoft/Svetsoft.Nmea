using System;

namespace Svetsoft.Geography.Positioning
{
    internal static class StringExtensions
    {
        /// <summary>
        ///     Returns a value indicating whether a specified substring occurs within this string. A parameter specifies the
        ///     culture, case, and sort rules used in the comparison.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <param name="index">
        ///     When this method returns, contains the zero-based index of the first occurrence of the specified
        ///     <paramref name="value" /> in the current string, if that string is found, or -1 if it is not. If
        ///     <paramref name="value" /> is <see cref="String.Empty" /> the index will be 0. This parameter is passed
        ///     uninitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        ///     <bold>true</bold> if the <paramref name="value" /> parameter occurs within this string, or if
        ///     <paramref name="value" /> is the empty string (""); otherwise, <bold>false</bold>.
        /// </returns>
        public static bool Contains(this string source, string value, StringComparison comparisonType, out int index)
        {
            index = source.IndexOf(value, comparisonType);

            return index >= 0;
        }

        /// <summary>
        ///     Returns a value indicating whether a specified Unicode character occurs within this string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The Unicode character to seek.</param>
        /// <param name="index">
        ///     When this method returns, contains the zero-based index of the first occurrence of the specified
        ///     <paramref name="value" /> in the current string, if that value is found, or -1 if it is not. This parameter is
        ///     passed uninitialized; any value originally supplied in result will be overwritten.
        /// </param>
        /// <returns>
        ///     <bold>true</bold> if the <paramref name="value" /> parameter occurs within this string, or if
        ///     <paramref name="value" /> is the empty string (""); otherwise, <bold>false</bold>.
        /// </returns>
        public static bool Contains(this string source, char value, out int index)
        {
            index = source.IndexOf(value);

            return index >= 0;
        }

        /// <summary>
        ///     Returns a value indicating whether a specified Unicode character occurs within this string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The Unicode character to seek.</param>
        /// <returns>
        ///     <bold>true</bold> if the <paramref name="value" /> parameter occurs within this string, or if
        ///     <paramref name="value" /> is the empty string (""); otherwise, <bold>false</bold>.
        /// </returns>
        public static bool Contains(this string source, char value)
        {
            return source.IndexOf(value) >= 0;
        }
    }
}