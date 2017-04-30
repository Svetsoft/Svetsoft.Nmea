using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for fix data.
    /// </summary>
    public class GnsSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GnsSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GnsSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the time of day, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Returns the position.
        /// </summary>
        public Position Position { get; internal set; }

        /// <summary>
        ///     Returns the fix mode.
        /// </summary>
        public FixMode FixMode { get; internal set; }

        /// <summary>
        ///     Returns the number of satellites in use.
        /// </summary>
        public double SatellitesInUse { get; internal set; }

        /// <summary>
        ///     Returns the HDROP.
        /// </summary>
        public double Hdrop { get; internal set; }

        /// <summary>
        ///     Returns the antenna altitude.
        /// </summary>
        public Distance AntennaAltitude { get; internal set; }

        /// <summary>
        ///     Returns the geoidal separation.
        /// </summary>
        public Distance GeoidalSeparation { get; internal set; }

        /// <summary>
        ///     Returns the differential data.
        /// </summary>
        public DifferentialData DifferentialData { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GnsSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            Position = GetPosition(1);
            FixMode = GetFixMode(5);
            SatellitesInUse = GetDouble(6);
            Hdrop = GetDouble(7);
            AntennaAltitude = GetDistance(8, DistanceUnit.Meters);
            GeoidalSeparation = GetDistance(9, DistanceUnit.Meters);
            DifferentialData = GetDifferentialData(10);
        }
    }
}