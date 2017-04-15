using System;
using System.Linq;
using Svetsoft.Geography.Positioning.Extensions;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a GPRMC sentence of the NMEA specification with details about geographic position (latitude/longitude)
    ///     and time.
    /// </summary>
    public class GprmcSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GprmcSentence" /> class.
        /// </summary>
        /// <param name="nmeaSentence">The <see cref="NmeaSentence" /> to copy values from.</param>
        public GprmcSentence(NmeaSentence nmeaSentence)
            : base(nmeaSentence)
        {
        }

        /// <summary>
        ///     Returns the date and time of the fix, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public DateTime FixUtcDateTime { get; internal set; }

        /// <summary>
        ///     Returns the position in this cycle.
        /// </summary>
        public Position Position { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="NavigationState" /> of this sentence.
        /// </summary>
        public NavigationState NavigationState { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="Speed" /> in this cycle.
        /// </summary>
        public Speed Speed { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="Longitude">MagneticVariation</see> in this cycle.
        /// </summary>
        public Longitude MagneticVariation { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="Azimuth">Bearing</see> in this cycle.
        /// </summary>
        public Azimuth Bearing { get; internal set; }
    }
}