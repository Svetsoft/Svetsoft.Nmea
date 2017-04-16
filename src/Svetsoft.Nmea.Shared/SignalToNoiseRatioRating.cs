namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Indicates a description of the rating for a signal-to-noise ratio.
    /// </summary>
    public enum SignalToNoiseRatioRating
    {
        /// <summary>
        ///     Represents a value where the signal is mostly obscured possibly by physical elements but might briefly be part of a
        ///     fix.
        /// </summary>
        Poor,

        /// <summary>
        ///     Represents a value where the signal is partially obscured, but could be part of a sustained fix.
        /// </summary>
        Moderate,

        /// <summary>
        ///     Represents a value where the signal has little interference and can maintain a reliable fix.
        /// </summary>
        Good,

        /// <summary>
        ///     Represents a value where the signal is in direct line of sight from the receiver and can sustain a fix.
        /// </summary>
        Excellent
    }
}