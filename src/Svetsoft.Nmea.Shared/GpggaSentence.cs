using System;
using System.Linq;
using Svetsoft.Nmea.Extensions;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a GPGGA sentence of the NMEA specification with details about geographic position (latitude/longitude)
    ///     and time.
    /// </summary>
    public class GpggaSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GpggaSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GpggaSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the time of day of this position, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Returns the position in this cycle.
        /// </summary>
        public Position Position { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="FixQuality" /> in this cycle.
        /// </summary>
        public FixQuality FixQuality { get; internal set; }

        /// <summary>
        ///     Returns the number of visible satellites in this cycle.
        /// </summary>
        public int VisibleSatellitesCount { get; internal set; }

        /// <summary>
        ///     Returns the horizontal <see cref="DilutionOfPrecision" />.
        /// </summary>
        public DilutionOfPrecision HorizontalDilutionOfPrecision { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="Distance">Altitude</see> in this cycle.
        /// </summary>
        public Distance Altitude { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="Distance">Geoidal Separator</see> in this cycle.
        /// </summary>
        public Distance GeoidalSeparator { get; internal set; }

        /// <summary>
        ///     Returns the age of differential GPS data described in seconds since the last SC104 (type 1 or 9) update. If
        ///     Differential GPS is not used, this value is null.
        /// </summary>
        public TimeSpan? SecondsSinceLastDifferentialGpsSc104Update { get; internal set; }

        /// <summary>
        ///     Returns the differential reference station ID.
        /// </summary>
        public int? DifferentialGpsReferenceStationId { get; internal set; }

        /// <summary>
        ///     Converts a GPGGA sentence to its <see cref="GpggaSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;

            // UTC time of position
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                var utcString = fields[0];
                var utcHours = int.Parse(utcString.Substring(0, 2));
                var utcMinutes = int.Parse(utcString.Substring(2, 2));
                var utcSeconds = int.Parse(utcString.Substring(4, 2));
                if (utcString.Contains(TimeSpanMillisecondsDelimiter, out int utcMillisecondsIndex))
                {
                    var utcMilliseconds = int.Parse(utcString.Substring(utcMillisecondsIndex + 1, utcString.Length - (utcMillisecondsIndex + 1))) * 1000;
                    UtcTime = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
                }
                else
                {
                    UtcTime = new TimeSpan(utcHours, utcMinutes, utcSeconds);
                }
            }

            // Position (latitude/longitude)
            if (fields.Length > 4 && fields.All(Enumerable.Range(1, 4), s => !string.IsNullOrWhiteSpace(s)))
            {
                Position = Position.Parse(fields.ToArray(1, 4));
            }

            // Fix Quality
            if (fields.Length > 5 && !string.IsNullOrWhiteSpace(fields[5]))
            {
                FixQuality = Fix.ParseQuality(fields[5]);
            }

            // Total number of visible satellites
            if (fields.Length > 6 && !string.IsNullOrWhiteSpace(fields[6]))
            {
                VisibleSatellitesCount = int.Parse(fields[6]);
            }

            // Horizontal Dilution of Precision
            if (fields.Length > 7 && !string.IsNullOrWhiteSpace(fields[7]))
            {
                HorizontalDilutionOfPrecision = DilutionOfPrecision.Parse(fields[7]);
            }

            // Altitude
            if (fields.Length > 8 && !string.IsNullOrWhiteSpace(fields[8]) && !string.IsNullOrWhiteSpace(fields[9]))
            {
                Altitude = Distance.ParseDistance(Distance.ParseUnit(fields[9]), fields[8]);
            }

            // Geoidal Separator
            if (fields.Length > 9 && !string.IsNullOrWhiteSpace(fields[10]) && !string.IsNullOrWhiteSpace(fields[11]))
            {
                GeoidalSeparator = Distance.ParseDistance(Distance.ParseUnit(fields[11]), fields[10]);
            }

            // Verify that the differential GPS exists; otherwise use an empty value
            if (fields.Length >= 14 && !string.IsNullOrWhiteSpace(fields[12]) && !string.IsNullOrWhiteSpace(fields[13]))
            {
                SecondsSinceLastDifferentialGpsSc104Update = TimeSpan.FromSeconds(float.Parse(fields[12]));
                DifferentialGpsReferenceStationId = int.Parse(fields[13]);
            }
            else
            {
                SecondsSinceLastDifferentialGpsSc104Update = null;
                DifferentialGpsReferenceStationId = null;
            }
        }
    }
}