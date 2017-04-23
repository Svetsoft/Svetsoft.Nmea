using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a rate at which an object moves.
    /// </summary>
    public class Speed
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Speed" /> class.
        /// </summary>
        /// <param name="unit">The <see cref="SpeedUnit" /> in which the speed is represented.</param>
        /// <param name="value">The value that the speed represents.</param>
        public Speed(SpeedUnit unit, double value)
        {
            Unit = unit;
            Value = value;
        }

        /// <summary>
        ///     The value that this speed represents.
        /// </summary>
        public double Value { get; }

        /// <summary>
        ///     Returns the <see cref="SpeedUnit" /> in which this speed is represented.
        /// </summary>
        public SpeedUnit Unit { get; }

        /// <summary>
        ///     Converts a string to its value equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The value equivalent of the string.</returns>
        public static double Parse(string value)
        {
            return double.Parse(value);
        }

        /// <summary>
        ///     Converts a string to its <see cref="Speed" /> equivalent.
        /// </summary>
        /// <param name="unit">A <see cref="SpeedUnit" /> in which <paramref name="value" /> is represented.</param>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Speed" /> equivalent of the string.</returns>
        public static Speed Parse(SpeedUnit unit, string value)
        {
            return new Speed(unit, Parse(value));
        }

        /// <summary>
        ///     Converts an array of string elements to its <see cref="Speed" /> equivalent.
        /// </summary>
        /// <param name="values">An array of string elements containing a value to convert.</param>
        /// <returns>The <see cref="Speed" /> equivalent to the elements.</returns>
        public static Speed Parse(string[] values)
        {
            if (values.Length != 2)
            {
                throw new FormatException("Invalid speed format");
            }

            return new Speed(SpeedUnit.Parse(values[1]), Parse(values[0]));
        }
    }
}