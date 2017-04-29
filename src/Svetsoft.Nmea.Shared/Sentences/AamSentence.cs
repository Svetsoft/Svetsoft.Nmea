namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for destination waypoint arrival alarm.
    /// </summary>
    public class AamSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="AamSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public AamSentence(string sentence)
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
        ///     Returns the waypoint.
        /// </summary>
        public Waypoint Waypoint { get; internal set; }

        /// <summary>
        ///     Returns the unit of radius.
        /// </summary>
        public DistanceUnit RadiusUnit { get; internal set; }

        /// <summary>
        ///     Returns the radius of arrival.
        /// </summary>
        public double ArrivalCircleRadius { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="AamSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            IsArrivalCircleEntered = GetStatus(0);
            IsPerpendicularPassedAtWaypoint = GetStatus(1);
            ArrivalCircleRadius = GetDouble(2);
            RadiusUnit = GetDistanceUnit(3);
            Waypoint = GetWaypoint(4);
        }
    }
}