using System;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a GPGLL sentence of the NMEA specification with details about geographic position (latitude/longitude)
    ///     and time.
    /// </summary>
    public class GpgllSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GpgllSentence" /> class.
        /// </summary>
        /// <param name="nmeaSentence">The <see cref="NmeaSentence" /> to copy values from.</param>
        public GpgllSentence(NmeaSentence nmeaSentence)
            : base(nmeaSentence)
        {
        }

        /// <summary>
        ///     Returns the time of day of this position, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Returns whether this is a position fix.
        /// </summary>
        public bool IsFix { get; internal set; }

        /// <summary>
        ///     Returns the position in this cycle.
        /// </summary>
        public Position Position { get; internal set; }
    }
}