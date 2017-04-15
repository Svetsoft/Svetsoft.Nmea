using System;

namespace Svetsoft.Geography.Positioning
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
        /// <param name="nmeaSentence">The <see cref="NmeaSentence" /> to copy values from.</param>
        public GpggaSentence(NmeaSentence nmeaSentence)
            : base(nmeaSentence)
        {
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
        /// Returns the differential reference station ID.
        /// </summary>
        public int? DifferentialGpsReferenceStationId { get; internal set; }
    }
}