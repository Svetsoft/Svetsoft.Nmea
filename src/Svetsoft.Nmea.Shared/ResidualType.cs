using System;
using System.Collections.Generic;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents whether a type of GPS range residual.
    /// </summary>
    public struct ResidualType
    {
        static ResidualType()
        {
            InternalList = new[]
            {
                UsedInSentence,
                CalculatedAfterSentence
            };
        }

        private static readonly IList<ResidualType> InternalList;

        /// <summary>
        ///     Represents the position fix is not available.
        /// </summary>
        public static readonly ResidualType UsedInSentence = new ResidualType(0);

        /// <summary>
        ///     Represents the residuals calculated after a NMEA sentence.
        /// </summary>
        public static readonly ResidualType CalculatedAfterSentence = new ResidualType(1);

        /// <summary>
        ///     Returns the value that this residual type represents.
        /// </summary>
        internal int Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResidualType" /> class.
        /// </summary>
        /// <param name="value">The value that the residual type represents.</param>
        internal ResidualType(int value)
        {
            Value = value;
        }

        /// <summary>
        ///     Converts a string to its <see cref="ResidualType" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="ResidualType" /> equivalent of the string.</returns>
        public static ResidualType Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException($"{nameof(value)} is not in the correct format");
            }

            var parsedValue = int.Parse(value);
            foreach (var item in InternalList)
            {
                if (parsedValue == item.Value)
                {
                    return item;
                }
            }

            throw new FormatException($"{nameof(value)} is not in the correct format");
        }
    }
}