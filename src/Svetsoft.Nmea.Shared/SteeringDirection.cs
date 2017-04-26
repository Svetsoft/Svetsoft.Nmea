using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents the direction in which an object is guided.
    /// </summary>
    public struct SteeringDirection
    {
        static SteeringDirection()
        {
            if (InternalList == null)
            {
                InternalList = new[]
                {
                    Left,
                    Right
                };
            }
        }

        private static readonly IList<SteeringDirection> InternalList;

        /// <summary>
        ///     Returns a read-only list of steering directions.
        /// </summary>
        public ReadOnlyCollection<SteeringDirection> List
        {
            get { return new ReadOnlyCollection<SteeringDirection>(InternalList); }
        }

        /// <summary>
        ///     Represents the direction indicated towards the left side. This field is read-only.
        /// </summary>
        public static readonly SteeringDirection Left = new SteeringDirection("L");

        /// <summary>
        ///     Represents the direction indicated towards the right side. This field is read-only.
        /// </summary>
        public static readonly SteeringDirection Right = new SteeringDirection("R");

        /// <summary>
        ///     Returns the value that this steering direction represents.
        /// </summary>
        internal string Value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SteeringDirection" /> class.
        /// </summary>
        /// <param name="value">The value that the steering direction represents.</param>
        internal SteeringDirection(string value)
        {
            Value = value;
        }

        /// <summary>
        ///     Converts a string to its <see cref="SteeringDirection" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="SteeringDirection" /> equivalent of the string.</returns>
        public static SteeringDirection Parse(string value)
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