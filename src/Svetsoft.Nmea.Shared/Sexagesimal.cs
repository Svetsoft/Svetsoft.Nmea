using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a numeric measurement with 60 as its base.
    /// </summary>
    public class Sexagesimal
    {
        protected const char NumberDecimalSeparator = '.';
        protected const string InfinityValue = "Infinity";
        protected const string EmptyValue = "Empty";

        /// <summary>
        ///     Represents the <see cref="Sexagesimal" /> zero. This field is read-only.
        /// </summary>
        public static readonly Sexagesimal Empty = new Sexagesimal(0.0);

        /// <summary>
        ///     Creates a new instance of the <see cref="Sexagesimal" /> class.
        /// </summary>
        /// <param name="degrees">The degrees as decimal fraction.</param>
        public Sexagesimal(double degrees)
        {
            Degrees = degrees;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Sexagesimal" /> class.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <param name="seconds">The seconds.</param>
        public Sexagesimal(int hours, int minutes, double seconds)
            : this(hours < 0 ? -(-hours + minutes / 60.0 + seconds / 3600.0) : hours + minutes / 60.0 + seconds / 3600.0)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Sexagesimal" /> class.
        /// </summary>
        /// <param name="hours">The hours.</param>
        /// <param name="decimalMinutes">The decimal minutes.</param>
        public Sexagesimal(int hours, double decimalMinutes)
            : this(hours < 0 ? -(-hours + decimalMinutes / 60.0) : hours + decimalMinutes / 60.0)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="Sexagesimal" /> class.
        /// </summary>
        /// <param name="hours">The hours.</param>
        public Sexagesimal(int hours)
            : this(Convert.ToDouble(hours))
        {
        }

        /// <summary>
        ///     Returns the degrees as decimal fraction.
        /// </summary>
        public double Degrees { get; }

        /// <summary>
        ///     Converts a string to its <see cref="Sexagesimal" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Sexagesimal" /> equivalent of the string.</returns>
        public static Sexagesimal Parse(string value)
        {
            return Parse(new[]
            {
                value
            });
        }

        /// <summary>
        ///     Converts an array of string elements to their <see cref="Sexagesimal" /> equivalent.
        /// </summary>
        /// <param name="values">An array of string elements containing values to convert.</param>
        /// <returns>The <see cref="Sexagesimal" /> equivalent of the elements.</returns>
        public static Sexagesimal Parse(string[] values)
        {
            // The number of elements defines the format
            switch (values.Length)
            {
                case 1:
                    // Degrees as decimal
                    if (string.Equals(values[0], EmptyValue, StringComparison.Ordinal))
                    {
                        return new Sexagesimal(0.0);
                    }

                    if (string.Equals(values[0], InfinityValue, StringComparison.Ordinal))
                    {
                        return new Sexagesimal(double.PositiveInfinity);
                    }

                    return new Sexagesimal(double.Parse(values[0]));
                case 2:
                    // Hours and decimal minutes
                    if (values[0].IndexOf(NumberDecimalSeparator) != -1)
                    {
                        throw new ArgumentException("Only the right-most number of a sexagesimal measurement can be a fractional value", nameof(values));
                    }

                    return new Sexagesimal(int.Parse(values[0]), float.Parse(values[1]));
                case 3:
                    // Hours, minutes and seconds
                    if (values[0].IndexOf(NumberDecimalSeparator) != -1 || values[0].IndexOf(NumberDecimalSeparator) != -1)
                    {
                        throw new ArgumentException("Only the right-most number of a sexagesimal measurement can be a fractional value", nameof(values));
                    }

                    return new Sexagesimal(int.Parse(values[0]), int.Parse(values[1]), double.Parse(values[2]));
                default:
                    return new Sexagesimal(0.0);
            }
        }

        /// <summary>
        ///     Converts a sexagesimal measurement to its managed equivalent. A return value indicates whether the conversion
        ///     succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="Sexagesimal" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParse(string value, out Sexagesimal result)
        {
            result = default(Sexagesimal);
            try
            {
                result = Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a position to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="values">An array of string elements containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="Sexagesimal" /> equivalent of the message
        ///     contained in <paramref name="values" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="values" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="values" /> parameter was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParse(string[] values, out Sexagesimal result)
        {
            result = default(Sexagesimal);
            try
            {
                result = Parse(values);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}