namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a comparison measurement of the level of a desired signal to the level of background noise.
    /// </summary>
    public class SignalToNoiseRatio
    {
        protected const char RatingDelimiter = '(';
        protected const string DecibelIdentifier = "DB";

        /// <summary>
        ///     Represents the empty <see cref="SignalToNoiseRatio" />. This field is read-only.
        /// </summary>
        public static readonly SignalToNoiseRatio Empty = new SignalToNoiseRatio(0);

        /// <summary>
        ///     Creates a new instance of the <see cref="SignalToNoiseRatio" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public SignalToNoiseRatio(int value)
        {
            Value = value;
            Rating = value.ToSignalToNoiseRatioRating();
        }

        /// <summary>
        ///     Returns the value of this signal-to-noise ratio.
        /// </summary>
        public int Value { get; }

        /// <summary>
        ///     Returns the rating of this signal-to-noise ratio.
        /// </summary>
        public SignalToNoiseRatioRating Rating { get; }

        /// <summary>
        ///     Converts a string to its <see cref="SignalToNoiseRatio" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="SignalToNoiseRatio" /> equivalent of the string.</returns>
        public static SignalToNoiseRatio Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new SignalToNoiseRatio(0);
            }

            // Remove clutter from input
            value = ClearInput(value);

            // If there's a rating in there, get rid of it
            if (value.Contains(RatingDelimiter, out int ratingIndex))
            {
                return new SignalToNoiseRatio(int.Parse(value.Substring(0, ratingIndex)));
            }

            return new SignalToNoiseRatio(int.Parse(value));
        }

        /// <summary>
        ///     Removes clutter from input and prepares <paramref name="value" /> to be parsed.
        /// </summary>
        /// <param name="value">The value to clear.</param>
        /// <returns>The cleared input.</returns>
        internal static string ClearInput(string value)
        {
            return value.ToUpper()
                        .Replace(DecibelIdentifier, string.Empty)
                        .Trim();
        }
    }
}