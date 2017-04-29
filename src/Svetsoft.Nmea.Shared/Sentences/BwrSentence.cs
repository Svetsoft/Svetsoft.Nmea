using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for bearing and distance to waypoint (Rhumb line).
    /// </summary>
    public class BwrSentence : NmeaSentence, ITrueBearingSentence, IMagneticBearingSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="BwrSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public BwrSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the true bearing.
        /// </summary>
        public Bearing TrueBearing { get; internal set; }

        /// <summary>
        ///     Returns the magnetic bearing.
        /// </summary>
        public Bearing MagneticBearing { get; set; }

        /// <summary>
        ///     Returns the waypoint.
        /// </summary>
        public Waypoint Waypoint { get; internal set; }

        /// <summary>
        ///     Returns the position of the waypoint.
        /// </summary>
        public Position WaypointPosition { get; internal set; }

        /// <summary>
        ///     Returns the time of day, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="Distance" /> to the waypoint.
        /// </summary>
        public Distance WaypointDistance { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="BwrSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            WaypointPosition = GetPosition(1);
            TrueBearing = GetBearing(5);
            MagneticBearing = GetBearing(7);
            WaypointDistance = GetDistance(9);
            Waypoint = GetWaypoint(11);
        }
    }
}