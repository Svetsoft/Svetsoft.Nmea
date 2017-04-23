using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents the height of a spacecraft in orbit above a surface.
    /// </summary>
    public class Elevation
    {
        protected static readonly char[] AngleValueDelimiters =
        {
            '°',
            ' ',
            '\'',
            '\"'
        };

        /// <summary>
        ///     Represents the empty <see cref="Elevation" />. This field is read-only.
        /// </summary>
        public static readonly Elevation Empty = new Elevation(Sexagesimal.Empty);

        /// <summary>
        ///     Creates a new instance of the <see cref="Elevation" /> class.
        /// </summary>
        /// <param name="sexagesimal">The <see cref="Sexagesimal" /> value.</param>
        public Elevation(Sexagesimal sexagesimal)
        {
            Sexagesimal = sexagesimal;
        }

        /// <summary>
        ///     Returns the <see cref="Sexagesimal" /> value of this elevation.
        /// </summary>
        public Sexagesimal Sexagesimal { get; }

        /// <summary>
        ///     Converts a string to its <see cref="Elevation" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Elevation" /> equivalent of the string.</returns>
        public static Elevation Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new Elevation(Sexagesimal.Empty);
            }

            try
            {
                var values = value.Trim()
                                  .Split(AngleValueDelimiters);
                return new Elevation(Sexagesimal.Parse(values));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid angle of elevation", ex);
            }
        }
    }
}