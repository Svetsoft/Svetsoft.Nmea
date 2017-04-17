using System;
using System.Linq;
using Svetsoft.Nmea.Extensions;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about recommended minimum navigation information.
    /// </summary>
    public class GprmcSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GprmcSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GprmcSentence(string sentence)
            : base(sentence)
        {
            Parse();
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

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GprmcSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;

            // UTC time of position
            var timeSpan = TimeSpan.Zero;
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                var utcTimeString = fields[0];
                var utcHours = int.Parse(utcTimeString.Substring(0, 2));
                var utcMinutes = int.Parse(utcTimeString.Substring(2, 2));
                var utcSeconds = int.Parse(utcTimeString.Substring(4, 2));
                if (utcTimeString.Contains(TimeSpanMillisecondsDelimiter, out int utcMillisecondsIndex))
                {
                    var utcMilliseconds = int.Parse(utcTimeString.Substring(utcMillisecondsIndex + 1, utcTimeString.Length - (utcMillisecondsIndex + 1))) * 1000;
                    timeSpan = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
                }
                else
                {
                    timeSpan = new TimeSpan(utcHours, utcMinutes, utcSeconds);
                }
            }

            // Fix mode
            if (fields.Length > 1 && !string.IsNullOrWhiteSpace(fields[1]))
            {
                NavigationState = Navigation.ParseNavigationState(fields[1]);
            }

            // Position (latitude/longitude)
            if (fields.Length > 2 && fields.All(Enumerable.Range(2, 4), s => !string.IsNullOrWhiteSpace(s)))
            {
                Position = Position.Parse(fields.ToArray(2, 4));
            }

            // Speed
            if (fields.Length > 6 && !string.IsNullOrWhiteSpace(fields[6]))
            {
                Speed = Speed.Parse(SpeedUnit.Knots, fields[6]);
            }

            // Bearing (Course)
            if (fields.Length > 7 && !string.IsNullOrWhiteSpace(fields[7]))
            {
                Bearing = Azimuth.ParseAzimuth(fields[7]);
            }

            // Parse the UTC date
            var date = DateTime.MinValue;
            if (fields.Length > 8 && !string.IsNullOrWhiteSpace(fields[8]))
            {
                var utcDateString = fields[8];
                var utcDay = int.Parse(utcDateString.Substring(0, 2));
                var utcMonth = int.Parse(utcDateString.Substring(2, 2));
                var utcYear = int.Parse(utcDateString.Substring(4, 2));
                date = new DateTime(2000 + utcYear, utcMonth, utcDay);
            }

            // Merge fix date & time values
            FixUtcDateTime = new DateTime(date.Year, date.Month, date.Day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            // Verify that a magnetic variation exists; otherwise use an empty value
            if (fields.Length > 10 && !string.IsNullOrWhiteSpace(fields[9]) && !string.IsNullOrWhiteSpace(fields[10]))
            {
                MagneticVariation = new Longitude(Sexagesimal.Parse(fields[9]), Longitude.ParseHemisphere(fields[10]));
            }
        }
    }
}