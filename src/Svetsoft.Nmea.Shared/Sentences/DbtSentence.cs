namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for depth below transducer.
    /// </summary>
    public class DbtSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DbtSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public DbtSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the depth in feet.
        /// </summary>
        public Distance FeetDepth { get; internal set; }

        /// <summary>
        ///     Returns the depth in meters.
        /// </summary>
        public Distance MetersDepth { get; internal set; }

        /// <summary>
        ///     Returns the depth in fathoms.
        /// </summary>
        public Distance FathomsDepth { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="DbtSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            FeetDepth = GetDistance(0);
            MetersDepth = GetDistance(2);
            FathomsDepth = GetDistance(4);
        }
    }
}