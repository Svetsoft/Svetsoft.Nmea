namespace Svetsoft.Geography.Positioning
{
    internal static class Int32Extensions
    {
        /// <summary>
        ///     Converts the numeric value of this instance to its equivalent rating of signal-to-noise ratio.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The <see cref="SignalToNoiseRatioRating" /> equivalent of the numeric value.</returns>
        public static SignalToNoiseRatioRating ToSignalToNoiseRatioRating(this int value)
        {
            if (value <= 15)
            {
                return SignalToNoiseRatioRating.Poor;
            }

            if (value <= 30)
            {
                return SignalToNoiseRatioRating.Moderate;
            }

            if (value <= 40)
            {
                return SignalToNoiseRatioRating.Good;
            }

            return SignalToNoiseRatioRating.Excellent;
        }
    }
}