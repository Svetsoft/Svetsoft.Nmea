using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a length of space between two points.
    /// </summary>
    public class Distance
    {
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
        ///     Converts an array of string elements to its <see cref="Distance" /> equivalent.
        /// </summary>
        /// <param name="values">An array of string elements containing a value to convert.</param>
        /// <returns>The <see cref="Distance" /> equivalent to the elements.</returns>
        public static Distance Parse(string[] values)
        {
            if (values.Length != 2)
            {
                throw new FormatException($"{nameof(values)} is not in the correct format");
            }

            return new Distance(DistanceUnit.Parse(values[1]), double.Parse(values[0]));
        }

        /// <summary>
        ///     Converts an array of string elements to its <see cref="Distance" /> equivalent.
        /// </summary>
        /// <param name="unit">The <see cref="DistanceUnit" /> in which the distance is represented.</param>
        /// <param name="value">An array of string elements containing a value to convert.</param>
        /// <returns>The <see cref="Distance" /> equivalent to the elements.</returns>
        public static Distance Parse(DistanceUnit unit, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException($"{nameof(value)} is not in the correct format");
            }

            return new Distance(unit, double.Parse(value));
        }
    }
}