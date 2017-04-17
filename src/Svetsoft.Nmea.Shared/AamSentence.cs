using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about destination waypoint arrival alarm.
    /// </summary>
    public class AamSentence : NmeaSentence
    {
        protected const string TrueString = "A";

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
        public bool IsArrivalCircleEntered { get; internal set; }

        /// <summary>
        ///     Returns whether the waypoint is passed perpendicularly.
        /// </summary>
        public bool IsPerpendicularPassedAtWaypoint { get; internal set; }

        /// <summary>
        ///     Returns the name of the waypoint.
        /// </summary>
        public string WaypointId { get; set; }

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
            var fields = Fields;

            // Arrival circle entered
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                IsArrivalCircleEntered = string.Equals(fields[0], TrueString, StringComparison.Ordinal);
            }

            // Perpendicular passed at waypoint
            if (fields.Length > 1 && !string.IsNullOrWhiteSpace(fields[1]))
            {
                IsPerpendicularPassedAtWaypoint = string.Equals(fields[1], TrueString, StringComparison.Ordinal);
            }

            // Arrival circle radius
            if (fields.Length > 2 && !string.IsNullOrWhiteSpace(fields[2]))
            {
                ArrivalCircleRadius = double.Parse(fields[2]);
            }

            // Units of radius
            if (fields.Length > 3 && !string.IsNullOrWhiteSpace(fields[3]))
            {
                RadiusUnit = Distance.ParseUnit(fields[3]);
            }

            // Waypoint ID
            if (fields.Length > 4 && !string.IsNullOrWhiteSpace(fields[4]))
            {
                WaypointId = fields[4];
            }
        }
    }
}