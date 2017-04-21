using System;
using System.Linq;
using Svetsoft.Nmea.Extensions;

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
        ///     Returns the name of the destination waypoint.
        /// </summary>
        public string DestinationWaypointId { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="BecSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;

            // UTC time of position
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                var utcString = fields[0];
                var utcHours = int.Parse(utcString.Substring(0, 2));
                var utcMinutes = int.Parse(utcString.Substring(2, 2));
                var utcSeconds = int.Parse(utcString.Substring(4, 2));
                if (utcString.Contains(TimeSpanMillisecondsDelimiter, out int utcMillisecondsIndex))
                {
                    var utcMilliseconds = int.Parse(utcString.Substring(utcMillisecondsIndex + 1, utcString.Length - (utcMillisecondsIndex + 1))) * 1000;
                    UtcTime = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
                }
                else
                {
                    UtcTime = new TimeSpan(utcHours, utcMinutes, utcSeconds);
                }
            }

            // Position (latitude/longitude)
            if (fields.Length > 4 && fields.All(Enumerable.Range(1, 4), s => !string.IsNullOrWhiteSpace(s)))
            {
                Position = Position.Parse(fields.ToArray(1, 4));
            }

            // True bearing
            if (fields.Length > 6 && fields.All(Enumerable.Range(5, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                TrueBearing = Bearing.Parse(fields.ToArray(5, 2));
            }

            // Magnetic bearing
            if (fields.Length > 8 && fields.All(Enumerable.Range(7, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                MagneticBearing = Bearing.Parse(fields.ToArray(7, 2));
            }

            // Speed
            if (fields.Length > 10 && fields.All(Enumerable.Range(9, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                Speed = Speed.Parse(fields.ToArray(9, 2));
            }

            // Destination waypoint ID
            if (fields.Length > 11 && !string.IsNullOrWhiteSpace(fields[11]))
            {
                DestinationWaypointId = fields[11];
            }
        }
    }
}