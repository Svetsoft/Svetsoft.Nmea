namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a value that satisfies statistical randomness.
    /// </summary>
    public struct PseudoRandomNoise
    {
        /// <summary>
        ///     Returns the value of this pseudo-random noise.
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     Returns the numeric representation of this pseudo-random noise.
        /// </summary>
        public int Number { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="PseudoRandomNoise" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="number">The numeric representation of <paramref name="value" />.</param>
        public PseudoRandomNoise(string value, int number)
        {
            Value = value;
            Number = number;
        }

        /// <summary>
        ///     Converts a string to its <see cref="PseudoRandomNoise" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="PseudoRandomNoise" /> equivalent of the string.</returns>
        public static PseudoRandomNoise Parse(string value)
        {
            var number = int.Parse(value);
            return new PseudoRandomNoise(value, number);
        }
    }
}