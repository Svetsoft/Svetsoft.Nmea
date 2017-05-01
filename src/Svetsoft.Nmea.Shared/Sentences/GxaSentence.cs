using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for TRANSIT position (latitude/longitude).
    /// </summary>
    public class GxaSentence : NmeaSentence, IUtcTimeSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GxaSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        [Obsolete("TRANSIT position (latitude/longitude) sentence (GXA) has been designated obsolete by NMEA as of v3.01.")]
        public GxaSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the <see cref="Position" />.
        /// </summary>
        public Position Position { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="Waypoint" />.
        /// </summary>
        public Waypoint Waypoint { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="PseudoRandomNoise" />.
        /// </summary>
        public PseudoRandomNoise PseudoRandomNoise { get; internal set; }

        /// <summary>
        ///     Returns the time of day, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GxaSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            Position = GetPosition(1);
            Waypoint = GetWaypoint(5);
            PseudoRandomNoise = GetPseudoRandomNoise(6);
        }
    }
}