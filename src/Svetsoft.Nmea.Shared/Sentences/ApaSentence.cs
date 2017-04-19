namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Autopilot (A).
    /// </summary>
    public sealed class ApaSentence : AutoPilotSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ApaSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public ApaSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }
    }
}