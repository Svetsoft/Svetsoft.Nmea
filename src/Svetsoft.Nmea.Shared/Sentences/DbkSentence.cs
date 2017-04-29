namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for depth below keel.
    /// </summary>
    public class DbkSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DbkSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public DbkSentence(string sentence)
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
        ///     Parses the fields of this sentence to its <see cref="DbkSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            FeetDepth = GetDistance(0);
            MetersDepth = GetDistance(2);
            FathomsDepth = GetDistance(4);
        }
    }
}