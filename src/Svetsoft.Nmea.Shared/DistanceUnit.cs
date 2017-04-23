using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a unit of measure for distance measurements.
    /// </summary>
    public struct DistanceUnit
    {
        private static readonly IList<DistanceUnit> InternalList = new[]
        {
            Meters,
            NauticalMiles,
            Kilometers
        };

        /// <summary>
        ///     Returns a read-only list of distance units.
        /// </summary>
        public ReadOnlyCollection<DistanceUnit> List
        {
            get { return new ReadOnlyCollection<DistanceUnit>(InternalList); }
        }

        /// <summary>
        ///     Returns the value that this distance unit represents.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DistanceUnit" /> class.
        /// </summary>
        /// <param name="value">The value that the distance unit represents.</param>
        internal DistanceUnit(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Represents the base unit of length in the International System of Units.
        /// </summary>
        public static readonly DistanceUnit Meters = new DistanceUnit("M");

        /// <summary>
        ///     Represents the unit of length also known as "Sea miles".
        /// </summary>
        public static readonly DistanceUnit NauticalMiles = new DistanceUnit("N");

        /// <summary>
        ///     Represents the unit of length in the International System of Units, equal to 1000 meters.
        /// </summary>
        public static readonly DistanceUnit Kilometers = new DistanceUnit("K");

        /// <summary>
        ///     Converts a string to its <see cref="DistanceUnit" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="DistanceUnit" /> equivalent of the string.</returns>
        public static DistanceUnit Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException($"{nameof(value)} is not in the correct format");
            }

            foreach (var item in InternalList)
            {
                if (value.Equals(item.Value, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }

            throw new FormatException($"{nameof(value)} is not in the correct format");
        }
    }
}