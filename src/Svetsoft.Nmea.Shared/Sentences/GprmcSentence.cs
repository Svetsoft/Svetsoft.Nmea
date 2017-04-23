using System;

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
        ///     Returns the navigation state of this sentence.
        /// </summary>
        public Status NavigationState { get; internal set; }

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
        public Bearing Bearing { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GprmcSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            NavigationState = GetStatus(1);
            Position = GetPosition(2);
            Speed = GetSpeed(6, SpeedUnit.Knots);
            Bearing = new Bearing(GetAzimuth(7), AbsoluteBearing.True);

            var timeSpan = GetUtcTime(0);
            var date = GetUtcDate(8);
            FixUtcDateTime = new DateTime(date.Year, date.Month, date.Day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            MagneticVariation = GetLongitude(9);
        }
    }
}