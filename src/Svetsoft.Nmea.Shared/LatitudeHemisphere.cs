using System;
using System.Collections.Generic;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a division of the globe into latitudinal marks.
    /// </summary>
    public struct LatitudeHemisphere
    {
        static LatitudeHemisphere()
        {
            if (InternalList == null)
            {
                InternalList = new[]
                {
                    North,
                    South
                };
            }
        }

        private static readonly IList<LatitudeHemisphere> InternalList;

        /// <summary>
        ///     Returns the value that this hemisphere represents.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LatitudeHemisphere" /> class.
        /// </summary>
        /// <param name="value">The value that the hemisphere representes.</param>
        internal LatitudeHemisphere(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Represents the hemisphere that is north of equator.
        /// </summary>
        public static readonly LatitudeHemisphere North = new LatitudeHemisphere("N");

        /// <summary>
        ///     Represents the hemisphere that is south of equator.
        /// </summary>
        public static readonly LatitudeHemisphere South = new LatitudeHemisphere("S");

        /// <summary>
        ///     Converts a string to its <see cref="LatitudeHemisphere" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="LatitudeHemisphere" /> equivalent of the string.</returns>
        public static LatitudeHemisphere Parse(string value)
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