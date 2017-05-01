using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for GPS pseudorange noise statistics.
    /// </summary>
    public class GstSentence : NmeaSentence, IUtcTimeSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GstSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GstSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the standard deviation of altitude error.
        /// </summary>
        public Distance AltitudeStdDeviation { get; internal set; }

        /// <summary>
        ///     Returns the standard deviation of longitude error.
        /// </summary>
        public Distance LongitudeStdDeviation { get; internal set; }

        /// <summary>
        ///     Returns the standard deviation of latitude error.
        /// </summary>
        public Distance LatitudeStdDeviation { get; internal set; }

        /// <summary>
        ///     Returns the standard deviation of semi-minor axis of error ellipse.
        /// </summary>
        public Distance SemiMinorStdDeviation { get; internal set; }

        /// <summary>
        ///     Returns the standard deviation of semi-major axis of error ellipse.
        /// </summary>
        public Distance SemiMajorStdDeviation { get; internal set; }

        /// <summary>
        ///     Returns the total RMS standard deviation of ranges inputs to the navigation solution.
        /// </summary>
        public string TotalRmsStdDeviation { get; internal set; }

        /// <summary>
        ///     Returns the orientation of semi-major axis of error ellipse.
        /// </summary>
        public Azimuth OrientationSemiMajor { get; internal set; }

        /// <summary>
        ///     Returns the time of day, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GstSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            TotalRmsStdDeviation = GetString(1);
            SemiMajorStdDeviation = GetDistance(2, DistanceUnit.Meters);
            SemiMinorStdDeviation = GetDistance(3, DistanceUnit.Meters);
            OrientationSemiMajor = GetAzimuth(4);
            LatitudeStdDeviation = GetDistance(5, DistanceUnit.Meters);
            LongitudeStdDeviation = GetDistance(6, DistanceUnit.Meters);
            AltitudeStdDeviation = GetDistance(7, DistanceUnit.Meters);
        }
    }
}