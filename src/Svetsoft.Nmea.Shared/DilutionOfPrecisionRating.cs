namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Indicates a description of the confidence level of precision of a measurement.
    /// </summary>
    public enum DilutionOfPrecisionRating
    {
        /// <summary>
        ///     Highest possible confidence level to be used for applications demanding the highest possible precision at all
        ///     times.
        /// </summary>
        Ideal,

        /// <summary>
        ///     Positional measurement is considered accurate enough to meet all but the most sensitive applications.
        /// </summary>
        Excellent,

        /// <summary>
        ///     Positional measurement can be used to make reliable in-route navigation.
        /// </summary>
        Good,

        /// <summary>
        ///     Positional measurements can be used for calculations, but the fix quality is recommended.
        /// </summary>
        Moderate,

        /// <summary>
        ///     Positional measurement should be discarded or used only to indicate a very rough estimate of the current location.
        /// </summary>
        Fair,

        /// <summary>
        ///     Measurement is highly inaccurate.
        /// </summary>
        Poor
    }
}