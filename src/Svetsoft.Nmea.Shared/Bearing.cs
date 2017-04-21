using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents the direction or course of an object.
    /// </summary>
    public class Bearing
    {
        private const char AbsoluteBearingMagneticDelimiter = 'M';
        private const char AbsoluteBearingTrueDelimiter = 'T';

        /// <summary>
        ///     Creates a new instance of the <see cref="Bearing" /> class.
        /// </summary>
        /// <param name="value">The value represented.</param>
        /// <param name="absoluteBearing">The <see cref="AbsoluteBearing" />.</param>
        public Bearing(Azimuth value, AbsoluteBearing absoluteBearing)
        {
            Value = value;
            AbsoluteBearing = absoluteBearing;
        }

        /// <summary>
        ///     Returns the value this bearing represents.
        /// </summary>
        public Azimuth Value { get; internal set; }

        /// <summary>
        ///     Returns the absolute value of this bearing.
        /// </summary>
        public AbsoluteBearing AbsoluteBearing { get; internal set; }

        /// <summary>
        ///     Converts a string to its <see cref="AbsoluteBearing" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="AbsoluteBearing" /> equivalent of the string.</returns>
        public static AbsoluteBearing ParseAbsoluteBearing(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid absolute bearing format");
            }

            if (value.Contains(AbsoluteBearingMagneticDelimiter))
            {
                return AbsoluteBearing.Magnetic;
            }

            if (value.Contains(AbsoluteBearingTrueDelimiter))
            {
                return AbsoluteBearing.True;
            }

            throw new FormatException("Invalid absolute bearing format");
        }

        /// <summary>
        ///     Converts an array of string elements to its <see cref="Bearing" /> equivalent.
        /// </summary>
        /// <param name="values">An array of string elements containing a value to convert.</param>
        /// <returns>The <see cref="Bearing" /> equivalent to the elements.</returns>
        public static Bearing Parse(string[] values)
        {
            if (values.Length != 2)
            {
                throw new FormatException("Invalid bearing format");
            }

            return new Bearing(Azimuth.ParseAzimuth(values[0]), ParseAbsoluteBearing(values[1]));
        }
    }
}