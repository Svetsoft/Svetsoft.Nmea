namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a raw that satisfies statistical randomness.
    /// </summary>
    public struct PseudoRandomNoise
    {
        /// <summary>
        ///     Returns the raw value of this pseudo-random noise.
        /// </summary>
        public string Raw { get; }

        /// <summary>
        ///     Returns the numeric representation of this pseudo-random noise.
        /// </summary>
        public int Number { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="PseudoRandomNoise" /> class.
        /// </summary>
        /// <param name="raw">The raw alue.</param>
        /// <param name="number">The numeric representation of <paramref name="raw" />.</param>
        public PseudoRandomNoise(string raw, int number)
        {
            Raw = raw;
            Number = number;
        }

        /// <summary>
        ///     Converts a string to its <see cref="PseudoRandomNoise" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a raw to convert.</param>
        /// <returns>The <see cref="PseudoRandomNoise" /> equivalent of the string.</returns>
        public static PseudoRandomNoise Parse(string value)
        {
            return new PseudoRandomNoise(value, int.Parse(value));
        }
    }
}