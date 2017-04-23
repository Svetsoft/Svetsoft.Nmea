namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a geographic coordinate that specifies a position in the longitudinal hemisphere.
    /// </summary>
    public class Longitude
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Longitude" /> class.
        /// </summary>
        /// <param name="sexagesimal">The <see cref="Sexagesimal" /> value.</param>
        /// <param name="hemisphere">The <see cref="LongitudeHemisphere"/> of the longitude.</param>
        public Longitude(Sexagesimal sexagesimal, LongitudeHemisphere hemisphere)
        {
            Sexagesimal = sexagesimal;
            Hemisphere = hemisphere;
        }

        /// <summary>
        ///     Returns the <see cref="Sexagesimal" /> value of this longitude.
        /// </summary>
        public Sexagesimal Sexagesimal { get; }

        /// <summary>
        /// Returns the <see cref="LongitudeHemisphere"/>.
        /// </summary>
        public LongitudeHemisphere Hemisphere { get; }
    }
}