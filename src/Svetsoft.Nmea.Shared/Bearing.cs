using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents the direction or course of an object.
    /// </summary>
    public class Bearing
    {
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

            return new Bearing(Azimuth.Parse(values[0]), AbsoluteBearing.Parse(values[1]));
        }
    }
}