using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Global Positioning System fix data. The sentence contains
    ///     time, position and fix related data for a GPS receiver.
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
        ///     Returns the time of day, expressed as the Coordinated Universal Time (UTC).
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
        ///     Returns the number of satellites in view in this cycle.
        /// </summary>
        public int SatellitesInViewCount { get; internal set; }

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
        ///     Returns the data of differential GPS.
        /// </summary>
        public DifferentialData DifferentialGpsData { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GpggaSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            Position = GetPosition(1);
            FixQuality = GetFixQuality(5);
            SatellitesInViewCount = GetInt32(6);
            HorizontalDilutionOfPrecision = GetDilutionOfPrecision(7);
            Altitude = GetDistance(8);
            GeoidalSeparator = GetDistance(10);
            DifferentialGpsData = GetDifferentialData(12);
        }
    }
}