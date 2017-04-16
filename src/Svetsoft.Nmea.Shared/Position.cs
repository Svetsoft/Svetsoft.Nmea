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
                throw new FormatException("Invalid position data format");
            }

            var sexagesimalLatitude = Sexagesimal.Parse(values[0]);
            var latitudeHemisphere = Latitude.ParseHemisphere(values[1]);

            var sexagesimalLongitude = Sexagesimal.Parse(values[2]);
            var longitudeHemisphere = Longitude.ParseHemisphere(values[3]);

            return new Position(new Latitude(sexagesimalLatitude, latitudeHemisphere), new Longitude(sexagesimalLongitude, longitudeHemisphere));
        }

        /// <summary>
        ///     Converts a position to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="values">An array of string elements containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="Position" /> equivalent of the message
        ///     contained in <paramref name="values" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="values" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns>
        ///     <bold>true</bold> if <paramref name="values" /> parameter was converted successfully; otherwise,
        ///     <bold>false</bold>.
        /// </returns>
        public static bool TryParse(string[] values, out Position result)
        {
            result = default(Position);
            try
            {
                result = Parse(values);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}