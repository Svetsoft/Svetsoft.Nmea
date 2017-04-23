using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a geographic location of an object.
    /// </summary>
    public class Position
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Position" /> class.
        /// </summary>
        /// <param name="latitude">The <see cref="Latitude" /> value of the position.</param>
        /// <param name="longitude">The <see cref="Longitude" /> value of the position.</param>
        private Position(Latitude latitude, Longitude longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        ///     Returns the <see cref="Latitude" /> portion of this position.
        /// </summary>
        public Latitude Latitude { get; }

        /// <summary>
        ///     Returns the <see cref="Longitude" /> portion of this position.
        /// </summary>
        public Longitude Longitude { get; }

        /// <summary>
        ///     Converts an array of string elements to its <see cref="Position" /> equivalent.
        /// </summary>
        /// <param name="values">An array of string elements containing a value to convert.</param>
        /// <returns>The <see cref="Position" /> equivalent to the elements.</returns>
        public static Position Parse(string[] values)
        {
            if (values.Length != 4)
            {
                throw new FormatException($"{nameof(values)} is not in the correct format");
            }

            var sexagesimalLatitude = Sexagesimal.Parse(values[0]);
            var latitudeHemisphere = LatitudeHemisphere.Parse(values[1]);

            var sexagesimalLongitude = Sexagesimal.Parse(values[2]);
            var longitudeHemisphere = LongitudeHemisphere.Parse(values[3]);

            return new Position(new Latitude(sexagesimalLatitude, latitudeHemisphere), new Longitude(sexagesimalLongitude, longitudeHemisphere));
        }
    }
}