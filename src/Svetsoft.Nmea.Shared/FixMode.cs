using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents whether a position fix is automatically computed in a two-dimensional (2D) or three-dimensional (3D)
    ///     plane or requires manual evaluation.
    /// </summary>
    public struct FixMode
    {
        private static readonly IList<FixMode> InternalList = new[]
        {
            Automatic,
            Manual
        };

        /// <summary>
        ///     Returns a read-only list of fix modes.
        /// </summary>
        public ReadOnlyCollection<FixMode> List
        {
            get { return new ReadOnlyCollection<FixMode>(InternalList); }
        }

        /// <summary>
        ///     Returns the value that this fix mode represents.
        /// </summary>
        internal string Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FixMode" /> class.
        /// </summary>
        /// <param name="value">The value that the fix mode represents.</param>
        internal FixMode(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     The position fix is automatically computed in a two-dimensional (2D) or three-dimensional (3D) plane.
        /// </summary>
        public static readonly FixMode Automatic = new FixMode("A");

        /// <summary>
        ///     The position fix requires manual evaluation.
        /// </summary>
        public static readonly FixMode Manual = new FixMode("M");

        /// <summary>
        ///     Converts a string to its <see cref="FixMode" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="FixMode" /> equivalent of the string.</returns>
        public static FixMode Parse(string value)
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