using System.Linq;
using Svetsoft.Nmea.Extensions;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Bearing - Waypoint to waypoint.
    /// </summary>
    public class BodSentence : NmeaSentence
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
        ///     Returns the name of the destination waypoint.
        /// </summary>
        public string DestinationWaypointId { get; internal set; }

        /// <summary>
        ///     Returns the name of the origin waypoint.
        /// </summary>
        public string OriginWaypointId { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="BodSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;

            // True bearing
            if (fields.Length > 1 && fields.All(Enumerable.Range(0, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                TrueBearing = Bearing.Parse(fields.ToArray(0, 2));
            }

            // Magnetic bearing
            if (fields.Length > 3 && fields.All(Enumerable.Range(2, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                MagneticBearing = Bearing.Parse(fields.ToArray(2, 2));
            }

            // Destination waypoint
            if (fields.Length > 4 && !string.IsNullOrWhiteSpace(fields[4]))
            {
                DestinationWaypointId = fields[4];
            }

            // Origin waypoint
            if (fields.Length > 5 && !string.IsNullOrWhiteSpace(fields[5]))
            {
                OriginWaypointId = fields[5];
            }
        }
    }
}