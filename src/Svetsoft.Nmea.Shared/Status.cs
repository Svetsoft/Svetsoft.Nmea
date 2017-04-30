using System;
using System.Collections.Generic;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents the status of an NMEA object.
    /// </summary>
    public class Status
    {
        static Status()
        {
            if (InternalList == null)
            {
                InternalList = new[]
                {
                    ActiveValue,
                    VoidValue
                };
            }
        }

        private static readonly IList<Status> InternalList;

        /// <summary>
        ///     Returns the value that this status represents.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Status" /> class.
        /// </summary>
        /// <param name="value">The value that the status represents.</param>
        internal Status(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Represents the active value of a <see cref="Status" />. This field is read-only.
        /// </summary>
        public static readonly Status ActiveValue = new Status("A");

        /// <summary>
        ///     Represents the void value of a <see cref="Status" />. This field is read-only.
        /// </summary>
        public static readonly Status VoidValue = new Status("V");

        /// <summary>
        ///     Converts a string to its <see cref="Status" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Status" /> equivalent of the string.</returns>
        public static Status Parse(string value)
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