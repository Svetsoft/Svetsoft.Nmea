using System.Linq;
using Svetsoft.Nmea.Extensions;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Autopilot (B).
    /// </summary>
    public sealed class ApbSentence : AutoPilotSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ApbSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public ApbSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the bearing of current position to destination.
        /// </summary>
        public Bearing BearingCurrentPositionToDestination { get; internal set; }

        /// <summary>
        ///     Returns the heading to steer to destination.
        /// </summary>
        public Bearing HeadingSteerToDestination { get; set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="ApbSentence" /> equivalent.
        /// </summary>
        protected override void Parse()
        {
            base.Parse();

            var fields = Fields;

            // Bearing current position to destination
            if (fields.Length > 11 && fields.All(Enumerable.Range(10, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                BearingCurrentPositionToDestination = Bearing.Parse(fields.ToArray(10, 2));
            }

            // Heading to steer to destination
            if (fields.Length > 13 && fields.All(Enumerable.Range(12, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                HeadingSteerToDestination = Bearing.Parse(fields.ToArray(12, 2));
            }
        }
    }
}