namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents an artificial object that aids locating other objects via positioning systems.
    /// </summary>
    public class Satellite
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Satellite" /> class.
        /// </summary>
        /// <param name="pseudoRandomNoise">The <see cref="PseudoRandomNoise" /> assigned to the satellite.</param>
        /// <param name="elevation">The current <see cref="Elevation" /> of the satellite.</param>
        /// <param name="azimuth">The current <see cref="Azimuth" /> of the satellite.</param>
        /// <param name="signalToNoiseRatio">The <see cref="SignalToNoiseRatio" /> of the satellite.</param>
        public Satellite(PseudoRandomNoise pseudoRandomNoise, Elevation elevation, Azimuth azimuth, SignalToNoiseRatio signalToNoiseRatio)
        {
            PseudoRandomNoise = pseudoRandomNoise;
            Elevation = elevation;
            Azimuth = azimuth;
            SignalToNoiseRatio = signalToNoiseRatio;
        }

        /// <summary>
        ///     Returns the <see cref="PseudoRandomNoise" /> assigned to this satellite.
        /// </summary>
        /// <remarks>This value can be used to uniquely identify this satellite when currently in orbit.</remarks>
        public PseudoRandomNoise PseudoRandomNoise { get; }

        /// <summary>
        ///     Returns the current <see cref="Elevation" /> of this satellite.
        /// </summary>
        public Elevation Elevation { get; }

        /// <summary>
        ///     Returns the current <see cref="Azimuth" /> of this satellite.
        /// </summary>
        public Azimuth Azimuth { get; }

        /// <summary>
        ///     Returns the <see cref="SignalToNoiseRatio" /> of this satellite.
        /// </summary>
        public SignalToNoiseRatio SignalToNoiseRatio { get; }
    }
}