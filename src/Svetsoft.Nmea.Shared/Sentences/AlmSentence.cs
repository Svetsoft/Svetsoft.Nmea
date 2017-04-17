using System;
using System.Globalization;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Global Positioning System almanac datas.
    /// </summary>
    public class AlmSentence : NmeaSentence
    {
        public readonly DateTime WeekNumberStartDate = new DateTime(1980, 01, 06);

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
        ///     Returns the Global Positioning System week number since <see cref="WeekNumberStartDate" />.
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
            var fields = Fields;

            // Total number of messages of this type in this cycle
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                MessagesCount = int.Parse(fields[0]);
            }

            // Message number
            if (fields.Length > 1 && !string.IsNullOrWhiteSpace(fields[1]))
            {
                MessageNumber = int.Parse(fields[1]);
            }

            // Satellite PRN number
            if (fields.Length > 2 && !string.IsNullOrWhiteSpace(fields[2]))
            {
                SatellitePrn = PseudoRandomNoise.Parse(fields[2]);
            }

            // GPS week number
            if (fields.Length > 3 && !string.IsNullOrWhiteSpace(fields[3]))
            {
                WeekNumber = double.Parse(fields[3]);
            }

            // SV health
            if (fields.Length > 4 && !string.IsNullOrWhiteSpace(fields[4]))
            {
                SatelliteHealth = int.Parse(fields[4], NumberStyles.HexNumber);
            }

            // Eccentricity
            if (fields.Length > 5 && !string.IsNullOrWhiteSpace(fields[5]))
            {
                Eccentricity = int.Parse(fields[5], NumberStyles.HexNumber);
            }

            // Almanac reference time
            if (fields.Length > 6 && !string.IsNullOrWhiteSpace(fields[6]))
            {
                AlmanacReferenceTime = int.Parse(fields[6], NumberStyles.HexNumber);
            }

            // Inclination angle
            if (fields.Length > 7 && !string.IsNullOrWhiteSpace(fields[7]))
            {
                InclinationAngle = int.Parse(fields[7], NumberStyles.HexNumber);
            }

            // Rate of right ascension
            if (fields.Length > 8 && !string.IsNullOrWhiteSpace(fields[8]))
            {
                RightAscensionRate = int.Parse(fields[8], NumberStyles.HexNumber);
            }

            // Root of semi-major axis
            if (fields.Length > 9 && !string.IsNullOrWhiteSpace(fields[9]))
            {
                SemiMajorAxisRoot = int.Parse(fields[9], NumberStyles.HexNumber);
            }

            // Argument of perigee
            if (fields.Length > 10 && !string.IsNullOrWhiteSpace(fields[10]))
            {
                PerigeeArgument = int.Parse(fields[10], NumberStyles.HexNumber);
            }

            // Longitude of ascension node
            if (fields.Length > 11 && !string.IsNullOrWhiteSpace(fields[11]))
            {
                AscensionNodeLongitude = int.Parse(fields[11], NumberStyles.HexNumber);
            }

            // Mean anomaly
            if (fields.Length > 12 && !string.IsNullOrWhiteSpace(fields[12]))
            {
                MeanAnomaly = int.Parse(fields[12], NumberStyles.HexNumber);
            }

            // F0 clock parameter
            if (fields.Length > 13 && !string.IsNullOrWhiteSpace(fields[13]))
            {
                F0ClockParameter = int.Parse(fields[13], NumberStyles.HexNumber);
            }

            // F1 clock parameter
            if (fields.Length > 14 && !string.IsNullOrWhiteSpace(fields[14]))
            {
                F1ClockParameter = int.Parse(fields[14], NumberStyles.HexNumber);
            }
        }
    }
}