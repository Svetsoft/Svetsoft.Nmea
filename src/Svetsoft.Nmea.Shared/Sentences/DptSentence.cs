namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for depth of water.
    /// </summary>
    public class DptSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DptSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public DptSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the depth in meters.
        /// </summary>
        public Distance Depth { get; internal set; }

        /// <summary>
        ///     Returns the offset from transducer. When this value is positive it means the distance is measured from tansducer to
        ///     water line. Otherwise, it means the distance is measured from transducer to keel.
        /// </summary>
        public double TransducerOffset { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="DptSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            Depth = GetDistance(0);
            TransducerOffset = GetDouble(2);
        }
    }
}