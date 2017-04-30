using System;
using System.Collections.Generic;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents whether an absolute navigation refers to magnetic or true bearing.
    /// </summary>
    public class AbsoluteBearing
    {
        static AbsoluteBearing()
        {
            if (InternalList == null)
            {
                InternalList = new[]
                {
                    Magnetic,
                    True
                };
            }
        }

        private static readonly IList<AbsoluteBearing> InternalList;

        /// <summary>
        ///     Represents the navigation measured in relation to magnetic north.
        /// </summary>
        public static readonly AbsoluteBearing Magnetic = new AbsoluteBearing("M");

        /// <summary>
        ///     Represents the navigation measured in relation to the fixed horizontal reference place of true north.
        /// </summary>
        public static readonly AbsoluteBearing True = new AbsoluteBearing("T");

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbsoluteBearing" /> class.
        /// </summary>
        /// <param name="value">The value that the absolute bearing represents.</param>
        internal AbsoluteBearing(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Returns the value that this absolute bearing represents.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        ///     Converts a string to its <see cref="AbsoluteBearing" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="AbsoluteBearing" /> equivalent of the string.</returns>
        public static AbsoluteBearing Parse(string value)
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