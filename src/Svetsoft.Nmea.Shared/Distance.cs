using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a length of space between two points.
    /// </summary>
    public class Distance
    {
        protected const char DistanceUnitMetersDelimiter = 'M';
        protected const char DistanceUnitNauticalMilesDelimiter = 'N';

        /// <summary>
        ///     Creates a new instance of the <see cref="Distance" /> class.
        /// </summary>
        /// <param name="unit">The <see cref="DistanceUnit" /> in which the distance is represented.</param>
        /// <param name="value">The value that the distance represents.</param>
        public Distance(DistanceUnit unit, double value)
        {
            Unit = unit;
            Value = value;
        }

        /// <summary>
        ///     Returns the <see cref="DistanceUnit" /> in which this distance is represented.
        /// </summary>
        public DistanceUnit Unit { get; }

        /// <summary>
        ///     The value that this distance represents.
        /// </summary>
        public double Value { get; }

        /// <summary>
        ///     Converts a string to its <see cref="DistanceUnit" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="DistanceUnit" /> equivalent of the string.</returns>
        public static DistanceUnit ParseUnit(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid distance unit format");
            }

            if (value.Contains(DistanceUnitMetersDelimiter))
            {
                return DistanceUnit.Meters;
            }

            if (value.Contains(DistanceUnitNauticalMilesDelimiter))
            {
                return DistanceUnit.NauticalMiles;
            }

            throw new FormatException("Invalid distance unit format");
        }

        /// <summary>
        ///     Converts a distance unit value to its managed equivalent. A return value indicates whether the conversion
        ///     succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="DistanceUnit" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseUnit(string value, out DistanceUnit result)
        {
            result = default(DistanceUnit);
            try
            {
                result = ParseUnit(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a string to its <see cref="Distance" /> equivalent.
        /// </summary>
        /// <param name="unit">A <see cref="DistanceUnit" /> in which <paramref name="value" /> is represented.</param>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Distance" /> equivalent of the string.</returns>
        public static Distance ParseDistance(DistanceUnit unit, string value)
        {
            return new Distance(unit, double.Parse(value));
        }

        /// <summary>
        ///     Converts a distance value to its managed equivalent. A return value indicates whether the conversion
        ///     succeeded.
        /// </summary>
        /// <param name="unit">A <see cref="DistanceUnit" /> in which <paramref name="value" /> is represented.</param>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="Distance" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseDistance(DistanceUnit unit, string value, out Distance result)
        {
            result = default(Distance);
            try
            {
                result = ParseDistance(unit, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}