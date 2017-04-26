using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents the unit of rate at which an object moves.
    /// </summary>
    public struct SpeedUnit
    {
        static SpeedUnit()
        {
            if (InternalList == null)
            {
                InternalList = new[]
                {
                    Knots
                };
            }
        }

        private static readonly IList<SpeedUnit> InternalList;

        /// <summary>
        ///     Returns a read-only list of speed units.
        /// </summary>
        public ReadOnlyCollection<SpeedUnit> List
        {
            get { return new ReadOnlyCollection<SpeedUnit>(InternalList); }
        }

        /// <summary>
        ///     Returns the value that this speed unit represents.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpeedUnit" /> class.
        /// </summary>
        /// <param name="value">The value that the speed unit represents.</param>
        internal SpeedUnit(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     The unit is represented as one nautical mile per hour.
        /// </summary>
        public static readonly SpeedUnit Knots = new SpeedUnit("N");

        /// <summary>
        ///     Converts a string to its <see cref="SpeedUnit" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="DistanceUnit" /> equivalent of the string.</returns>
        public static SpeedUnit Parse(string value)
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