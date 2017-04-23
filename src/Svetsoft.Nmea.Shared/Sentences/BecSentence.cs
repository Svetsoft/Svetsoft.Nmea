using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about bearing and distance to waypoint (Dead reckoning).
    /// </summary>
    public class BecSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="BecSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public BecSentence(string sentence)
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
        ///     Returns the true bearing.
        /// </summary>
        public Bearing TrueBearing { get; internal set; }

        /// <summary>
        ///     Returns the magnetic bearing.
        /// </summary>
        public Bearing MagneticBearing { get; set; }

        /// <summary>
        ///     Returns the <see cref="Speed" /> in this cycle.
        /// </summary>
        public Speed Speed { get; internal set; }

        /// <summary>
        ///     Returns the destination waypoint.
        /// </summary>
        public Waypoint DestinationWaypoint { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="BecSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            Position = GetPosition(1);
            TrueBearing = GetBearing(5);
            MagneticBearing = GetBearing(7);
            Speed = GetSpeed(9);
            DestinationWaypoint = GetWaypoint(11);
        }
    }
}