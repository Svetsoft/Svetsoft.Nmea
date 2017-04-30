using System;
using System.Collections.Generic;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a division of the globe into longitudinal marks.
    /// </summary>
    public struct LongitudeHemisphere
    {
        static LongitudeHemisphere()
        {
            if (InternalList == null)
            {
                InternalList = new[]
                {
                    East,
                    West
                };
            }
        }

        private static readonly IList<LongitudeHemisphere> InternalList;

        /// <summary>
        ///     Returns the value that this hemisphere represents.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LongitudeHemisphere" /> class.
        /// </summary>
        /// <param name="value">The value that the hemisphere representes.</param>
        internal LongitudeHemisphere(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Represents the hemisphere that is east of prime meridian and west of 180th meridian.
        /// </summary>
        public static readonly LongitudeHemisphere East = new LongitudeHemisphere("E");

        /// <summary>
        ///     Represents the hemisphere that is west of prime meridian and east of 180th meridian.
        /// </summary>
        public static readonly LongitudeHemisphere West = new LongitudeHemisphere("W");

        /// <summary>
        ///     Converts a string to its <see cref="LongitudeHemisphere" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="LongitudeHemisphere" /> equivalent of the string.</returns>
        public static LongitudeHemisphere Parse(string value)
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