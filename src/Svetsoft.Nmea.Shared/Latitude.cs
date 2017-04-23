namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a geographic coordinate that specifies a position in the latitudinal hemisphere.
    /// </summary>
    public class Latitude
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Latitude" /> class.
        /// </summary>
        /// <param name="sexagesimal">The <see cref="Sexagesimal" /> value.</param>
        /// <param name="hemisphere">The <see cref="LatitudeHemisphere"/> of the latitude.</param>
        public Latitude(Sexagesimal sexagesimal, LatitudeHemisphere hemisphere)
        {
            Sexagesimal = sexagesimal;
            Hemisphere = hemisphere;
        }

        /// <summary>
        ///     Returns the <see cref="Sexagesimal" /> value of this latitude.
        /// </summary>
        public Sexagesimal Sexagesimal { get; }

        /// <summary>
        /// Returns the <see cref="LatitudeHemisphere"/>.
        /// </summary>
        public LatitudeHemisphere Hemisphere { get; }
    }
}