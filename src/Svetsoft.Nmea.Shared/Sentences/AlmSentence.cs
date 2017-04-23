using System.Globalization;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Global Positioning System almanac datas.
    /// </summary>
    public class AlmSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="AlmSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public AlmSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the F0 clock parameter.
        /// </summary>
        public int F0ClockParameter { get; internal set; }

        /// <summary>
        ///     Returns the F1 clock parameter.
        /// </summary>
        public int F1ClockParameter { get; internal set; }

        /// <summary>
        ///     Returns the mean anomaly.
        /// </summary>
        public int MeanAnomaly { get; internal set; }

        /// <summary>
        ///     Returns the longitude of ascension node.
        /// </summary>
        public int AscensionNodeLongitude { get; internal set; }

        /// <summary>
        ///     Returns the argument of perigee.
        /// </summary>
        public int PerigeeArgument { get; internal set; }

        /// <summary>
        ///     Returns the root of semi-major axis.
        /// </summary>
        public int SemiMajorAxisRoot { get; internal set; }

        /// <summary>
        ///     Returns the rate of right ascension.
        /// </summary>
        public int RightAscensionRate { get; internal set; }

        /// <summary>
        ///     Returns the inclination angle.
        /// </summary>
        public int InclinationAngle { get; internal set; }

        /// <summary>
        ///     Returns the almanac reference time.
        /// </summary>
        public int AlmanacReferenceTime { get; internal set; }

        /// <summary>
        ///     Returns the eccentricity.
        /// </summary>
        public int Eccentricity { get; internal set; }

        /// <summary>
        ///     Returns the satellite's health.
        /// </summary>
        public int SatelliteHealth { get; internal set; }

        /// <summary>
        ///     Returns the Global Positioning System week number since <see cref="NmeaSentence.DateTimeBase" />.
        /// </summary>
        public double WeekNumber { get; internal set; }

        /// <summary>
        ///     Returns the satellite's PRN number.
        /// </summary>
        public PseudoRandomNoise SatellitePrn { get; internal set; }

        /// <summary>
        ///     Returns the message number associated with this sentence.
        /// </summary>
        public int MessageNumber { get; internal set; }

        /// <summary>
        ///     Returns the total number of messages of this type in this cycle.
        /// </summary>
        public int MessagesCount { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="AlmSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            MessagesCount = GetInt32(0);
            MessageNumber = GetInt32(1);
            SatellitePrn = GetPseudoRandomNoise(2);
            WeekNumber = GetDouble(3);
            SatelliteHealth = GetInt32(4, NumberStyles.HexNumber);
            Eccentricity = GetInt32(5, NumberStyles.HexNumber);
            AlmanacReferenceTime = GetInt32(6, NumberStyles.HexNumber);
            InclinationAngle = GetInt32(7, NumberStyles.HexNumber);
            RightAscensionRate = GetInt32(8, NumberStyles.HexNumber);
            SemiMajorAxisRoot = GetInt32(9, NumberStyles.HexNumber);
            PerigeeArgument = GetInt32(10, NumberStyles.HexNumber);
            AscensionNodeLongitude = GetInt32(11, NumberStyles.HexNumber);
            MeanAnomaly = GetInt32(12, NumberStyles.HexNumber);
            F0ClockParameter = GetInt32(13, NumberStyles.HexNumber);
            F1ClockParameter = GetInt32(14, NumberStyles.HexNumber);
        }
    }
}