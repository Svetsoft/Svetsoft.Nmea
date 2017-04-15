using System;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a set of methods that aid in determining the position of an object.
    /// </summary>
    public static class Fix
    {
        private const char FixModeAutomaticDelimiter = 'A';
        private const char FixModeManualDelimiter = 'M';
        private const char FixDelimiter = 'A';

        /// <summary>
        ///     Converts a string to a <see cref="bool" /> equivalent depending on whether a specified <paramref name="value"/> is a fix.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="bool" /> equivalent of the string.</returns>
        public static bool ParseFix(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid fix format");
            }

            return value.Contains(FixDelimiter);
        }

        /// <summary>
        ///     Converts a string to its <see cref="FixMode" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="FixMode" /> equivalent of the string.</returns>
        public static FixMode ParseMode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid fix mode format");
            }

            if (value.Contains(FixModeAutomaticDelimiter))
            {
                return FixMode.Automatic;
            }

            if (value.Contains(FixModeManualDelimiter))
            {
                return FixMode.Manual;
            }

            throw new FormatException("Invalid fix mode format");
        }

        /// <summary>
        ///     Converts a string to its <see cref="FixQuality" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="FixQuality" /> equivalent of the string.</returns>
        public static FixQuality ParseQuality(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid fix quality format");
            }

            var intValue = int.Parse(value);
            if (Enum.IsDefined(typeof(FixQuality), intValue))
            {
                return (FixQuality) intValue;
            }

            throw new FormatException("Invalid fix quality format");
        }

        /// <summary>
        ///     Converts an fix mode value to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="FixMode" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseMode(string value, out FixMode result)
        {
            result = default(FixMode);
            try
            {
                result = ParseMode(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a string to its <see cref="FixPlane" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="FixPlane" /> equivalent of the string.</returns>
        public static FixPlane ParsePlane(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid fix plane format");
            }

            switch (int.Parse(value))
            {
                case 0:
                    return FixPlane.NotAvailable;
                case 1:
                    return FixPlane.TwoDimensional;
                case 2:
                    return FixPlane.ThreeDimensional;
            }

            throw new FormatException("Invalid fix plane format");
        }

        /// <summary>
        ///     Converts an fix mode value to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="FixPlane" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParsePlane(string value, out FixPlane result)
        {
            result = default(FixPlane);
            try
            {
                result = ParsePlane(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}