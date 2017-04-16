using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a geographic coordinate that specifies a position in the longitudinal hemisphere.
    /// </summary>
    public class Longitude
    {
        protected const char EasternHemisphereDelimiter = 'E';
        protected const char WesternHemisphereDelimiter = 'W';

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

        /// <summary>
        ///     Converts a string to its <see cref="LongitudeHemisphere" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="LongitudeHemisphere" /> equivalent of the string.</returns>
        public static LongitudeHemisphere ParseHemisphere(string value)
        {
            if (value.Contains(EasternHemisphereDelimiter))
            {
                return LongitudeHemisphere.East;
            }

            if (value.Contains(WesternHemisphereDelimiter))
            {
                return LongitudeHemisphere.West;
            }

            throw new ArgumentException("Invalid longitude hemisphere", nameof(value));
        }
    }
}