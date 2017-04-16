using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a set of methods to aid determining the status of navigation.
    /// </summary>
    public static class Navigation
    {
        private const char NavigationStateValidDelimiter = 'A';
        private const char NavigationStateInvalidDelimiter = 'V';

        /// <summary>
        ///     Converts a string to its <see cref="NavigationState" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="NavigationState" /> equivalent of the string.</returns>
        public static NavigationState ParseNavigationState(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid navigation state format");
            }

            if (value.Contains(NavigationStateValidDelimiter))
            {
                return NavigationState.Valid;
            }

            if (value.Contains(NavigationStateInvalidDelimiter))
            {
                return NavigationState.Invalid;
            }

            throw new FormatException("Invalid navigation state format");
        }

        /// <summary>
        ///     Converts a navigation state value to its managed equivalent. A return value indicates whether the conversion
        ///     succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="NavigationState" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParse(string value, out NavigationState result)
        {
            result = default(NavigationState);
            try
            {
                result = ParseNavigationState(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}