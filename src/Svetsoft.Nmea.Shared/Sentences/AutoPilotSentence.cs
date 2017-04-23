namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Autopilot.
    /// </summary>
    public abstract class AutoPilotSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="AutoPilotSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        protected AutoPilotSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns whether the arrival circle is entered.
        /// </summary>
        public Status IsArrivalCircleEntered { get; internal set; }

        /// <summary>
        ///     Returns whether the waypoint is passed perpendicularly.
        /// </summary>
        public Status IsPerpendicularPassedAtWaypoint { get; internal set; }

        /// <summary>
        ///     Returns the destination waypoint.
        /// </summary>
        public Waypoint DestinationWaypoint { get; internal set; }

        /// <summary>
        ///     Returns the bearing of origin to destination.
        /// </summary>
        public Bearing BearingOriginToDestination { get; internal set; }

        /// <summary>
        ///     Returns the units of cross-track.
        /// </summary>
        public DistanceUnit CrossTrackUnits { get; internal set; }

        /// <summary>
        ///     Returns the steering direction in this cycle.
        /// </summary>
        public SteeringDirection SteeringDirection { get; internal set; }

        /// <summary>
        ///     Returns the error magnitude of cross-track.
        /// </summary>
        public double CrossTrackErrorMagnitude { get; internal set; }

        /// <summary>
        ///     Returns whether a Loran-C Cycle Lock device is used.
        /// </summary>
        public Status IsLoranCCycleLockActive { get; internal set; }

        /// <summary>
        ///     Returns whether a Loran-C Blink device is used.
        /// </summary>
        public Status IsLoranCBlinkActive { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="AutoPilotSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            IsLoranCBlinkActive = GetStatus(0);
            IsLoranCCycleLockActive = GetStatus(1);
            CrossTrackErrorMagnitude = GetDouble(2);
            SteeringDirection = GetSteeringDirection(3);
            CrossTrackUnits = GetDistanceUnit(4);
            IsArrivalCircleEntered = GetStatus(5);
            IsPerpendicularPassedAtWaypoint = GetStatus(6);
            BearingOriginToDestination = GetBearing(7);
            DestinationWaypoint = GetWaypoint(9);
        }
    }
}