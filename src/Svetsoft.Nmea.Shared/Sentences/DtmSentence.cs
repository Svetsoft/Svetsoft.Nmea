namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for datum reference.
    /// </summary>
    public class DtmSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DtmSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public DtmSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the Datum code.
        /// </summary>
        public string DatumCode { get; internal set; }

        /// <summary>
        ///     Returns the Datum subcode.
        /// </summary>
        public string DatumSubCode { get; internal set; }

        /// <summary>
        ///     Returns the Datum name.
        /// </summary>
        public string DatumName { get; internal set; }

        /// <summary>
        ///     Returns the altitude offset.
        /// </summary>
        public Distance AltitudeOffset { get; internal set; }

        /// <summary>
        ///     Returns the position offset.
        /// </summary>
        public Position PositionOffset { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="DtmSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            DatumCode = GetDatumCode(0);
            DatumSubCode = GetDatumSubCode(1);
            PositionOffset = GetPosition(2);
            AltitudeOffset = GetDistance(6, DistanceUnit.Meters);
            DatumName = GetDatumName(7);
        }
    }
}