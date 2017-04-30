using System;
using System.Collections.Generic;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents whether a position fix is automatically computed in a two-dimensional (2D) or three-dimensional (3D)
    ///     plane or requires manual evaluation.
    /// </summary>
    public struct FixMode
    {
        static FixMode()
        {
            if (InternalList == null)
            {
                InternalList = new[]
                {
                    Automatic,
                    Differential,
                    Estimate,
                    FloatRealTimeKinematic,
                    Manual,
                    NotInUseOrNotValid,
                    Precise,
                    RealTimeKinematic,
                    Simulation
                };
            }
        }

        private static readonly IList<FixMode> InternalList;

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
        ///     The position fix is differential.
        /// </summary>
        public static readonly FixMode Differential = new FixMode("D");

        /// <summary>
        ///     The position fix is precise (No degradation).
        /// </summary>
        public static readonly FixMode Precise = new FixMode("P");

        /// <summary>
        ///     The position fix is real time kinematic.
        /// </summary>
        public static readonly FixMode RealTimeKinematic = new FixMode("R");

        /// <summary>
        ///     The position fix is float real time kinematic.
        /// </summary>
        public static readonly FixMode FloatRealTimeKinematic = new FixMode("F");

        /// <summary>
        ///     The position fix is estimated (dead reckoning).
        /// </summary>
        public static readonly FixMode Estimate = new FixMode("E");

        /// <summary>
        ///     The constellation is not in use or is not valid.
        /// </summary>
        public static readonly FixMode NotInUseOrNotValid = new FixMode("N");

        /// <summary>
        ///     The position fix is simulated.
        /// </summary>
        public static readonly FixMode Simulation = new FixMode("S");

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