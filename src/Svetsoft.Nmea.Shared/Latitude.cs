using System;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a geographic coordinate that specifies a position in the latitudinal hemisphere.
    /// </summary>
    public class Latitude
    {
        protected const char NorthernHemisphereDelimiter = 'N';
        protected const char SouthernHemisphereDelimiter = 'S';

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

        /// <summary>
        ///     Converts a string to its <see cref="LatitudeHemisphere" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="LatitudeHemisphere" /> equivalent of the string.</returns>
        public static LatitudeHemisphere ParseHemisphere(string value)
        {
            if (value.Contains(NorthernHemisphereDelimiter))
            {
                return LatitudeHemisphere.North;
            }

            if (value.Contains(SouthernHemisphereDelimiter))
            {
                return LatitudeHemisphere.South;
            }

            throw new ArgumentException("Invalid latitude hemisphere", nameof(value));
        }
    }
}