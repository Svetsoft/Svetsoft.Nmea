namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for bearing (Waypoint to waypoint).
    /// </summary>
    public class BodSentence : NmeaSentence, ITrueBearingSentence, IMagneticBearingSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="BodSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public BodSentence(string sentence)
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
        ///     Returns the destination waypoint.
        /// </summary>
        public Waypoint DestinationWaypoint { get; internal set; }

        /// <summary>
        ///     Returns the origin waypoint.
        /// </summary>
        public Waypoint OriginWaypoint { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="BodSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            TrueBearing = GetBearing(0);
            MagneticBearing = GetBearing(2);
            DestinationWaypoint = GetWaypoint(4);
            OriginWaypoint = GetWaypoint(5);
        }
    }
}