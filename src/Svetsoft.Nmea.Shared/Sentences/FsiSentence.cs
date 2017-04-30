namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for frequency set information.
    /// </summary>
    public class FsiSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="FsiSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public FsiSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the power level.
        /// </summary>
        public string PowerLevel { get; internal set; }

        /// <summary>
        ///     Returns the communications node.
        /// </summary>
        public string CommunicationsNode { get; internal set; }

        /// <summary>
        ///     Returns the receiving frequency.
        /// </summary>
        public string ReceivingFrequency { get; internal set; }

        /// <summary>
        ///     Returns the transmitting frequency.
        /// </summary>
        public string TransmittingFrequency { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="FsiSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            TransmittingFrequency = GetString(0);
            ReceivingFrequency = GetString(0);
            CommunicationsNode = GetString(0);
            PowerLevel = GetString(0);
        }
    }
}