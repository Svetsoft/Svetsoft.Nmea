namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for Autopilot (B).
    /// </summary>
    public sealed class ApbSentence : AutoPilotSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ApbSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public ApbSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the bearing of current position to destination.
        /// </summary>
        public Bearing BearingCurrentPositionToDestination { get; internal set; }

        /// <summary>
        ///     Returns the heading to steer to destination.
        /// </summary>
        public Bearing HeadingSteerToDestination { get; set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="ApbSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            BearingCurrentPositionToDestination = GetBearing(10);
            HeadingSteerToDestination = GetBearing(12);
        }
    }
}