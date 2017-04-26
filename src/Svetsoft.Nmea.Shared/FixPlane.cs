using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents whether a position fix is computed in a two-dimensional (2D) or three-dimensional (3D) plane.
    /// </summary>
    public struct FixPlane
    {
        static FixPlane()
        {
            InternalList = new[]
            {
                NotAvailable,
                TwoDimensional,
                ThreeDimensional
            };
        }

        private static readonly IList<FixPlane> InternalList;

        /// <summary>
        ///     Returns a read-only list of supported planes of a fix.
        /// </summary>
        public ReadOnlyCollection<FixPlane> List
        {
            get { return new ReadOnlyCollection<FixPlane>(InternalList); }
        }

        /// <summary>
        ///     Represents the position fix is not available.
        /// </summary>
        public static readonly FixPlane NotAvailable = new FixPlane(0);

        /// <summary>
        ///     Represents the position fix is computed in a two-dimensional (2D) plane.
        /// </summary>
        public static readonly FixPlane TwoDimensional = new FixPlane(1);

        /// <summary>
        ///     Represents the position fix is computed in a three-dimensional (3D) plane.
        /// </summary>
        public static readonly FixPlane ThreeDimensional = new FixPlane(2);

        /// <summary>
        ///     Returns the value that this fix plane represents.
        /// </summary>
        internal int Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FixPlane" /> class.
        /// </summary>
        /// <param name="value">The value that the fix plane represents.</param>
        internal FixPlane(int value)
        {
            Value = value;
        }

        /// <summary>
        ///     Converts a string to its <see cref="FixPlane" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="FixPlane" /> equivalent of the string.</returns>
        public static FixPlane Parse(string value)
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