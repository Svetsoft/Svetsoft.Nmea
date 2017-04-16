using Svetsoft.Nmea.Extensions;
using System;
using System.Linq;

namespace Svetsoft.Nmea
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
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GpgllSentence(string sentence)
            : base(sentence)
        {
            Parse();
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

        /// <summary>
        ///     Converts a GPGLL sentence to its <see cref="GpgllSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;

            // Position (latitude/longitude)
            if (fields.Length > 3 && fields.All(Enumerable.Range(0, 4), s => !string.IsNullOrWhiteSpace(s)))
            {
                Position = Position.Parse(fields.ToArray(0, 4));
            }

            // UTC time of position
            if (fields.Length > 4 && !string.IsNullOrWhiteSpace(fields[4]))
            {
                var utcString = fields[4];
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

            // Position fix
            if (fields.Length > 5 && !string.IsNullOrWhiteSpace(fields[5]))
            {
                IsFix = Fix.ParseFix(fields[5]);
            }
        }
    }
}