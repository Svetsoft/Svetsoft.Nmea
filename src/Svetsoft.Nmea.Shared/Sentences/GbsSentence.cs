using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for GPS satellite fault detection.
    /// </summary>
    public class GbsSentence : NmeaSentence, IUtcTimeSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GbsSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GbsSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the expected error for latitude.
        /// </summary>
        public Distance LatitudeExpectedError { get; internal set; }

        /// <summary>
        ///     Returns the expected error for longitude.
        /// </summary>
        public Distance LongitudeExpectedError { get; internal set; }

        /// <summary>
        ///     Returns the expected error for altitude.
        /// </summary>
        public Distance AltitudeExpectedError { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="PseudoRandomNoise" /> of most likely failed satellite.
        /// </summary>
        public PseudoRandomNoise Prn { get; internal set; }

        /// <summary>
        ///     Returns the probability of missed detection for most likely failed satellite.
        /// </summary>
        public double MissedDetectionProbability { get; internal set; }

        /// <summary>
        ///     Returns the estimate of bias on most likely failed satellite.
        /// </summary>
        public Distance BiasEstimate { get; internal set; }

        /// <summary>
        ///     Returns the standard deviation of bias estimate.
        /// </summary>
        public double BiasEstimateDeviationStandard { get; internal set; }

        /// <summary>
        ///     Returns the time of day for the GGA or GNS fix associated, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GbsSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            LatitudeExpectedError = GetDistance(1, DistanceUnit.Meters);
            LongitudeExpectedError = GetDistance(2, DistanceUnit.Meters);
            AltitudeExpectedError = GetDistance(3, DistanceUnit.Meters);
            Prn = GetPseudoRandomNoise(4);
            MissedDetectionProbability = GetDouble(5);
            BiasEstimate = GetDistance(6, DistanceUnit.Meters);
            BiasEstimateDeviationStandard = GetDouble(7);
        }
    }
}