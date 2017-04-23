using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents an angular measurement between projected and reference vectors on a spherical coordinate system.
    /// </summary>
    public class Azimuth
    {
        /// <summary>
        ///     Represents the empty <see cref="Azimuth" />. This field is read-only.
        /// </summary>
        public static readonly Azimuth Empty = new Azimuth(Sexagesimal.Empty);

        /// <summary>
        ///     Creates a new instance of the <see cref="Azimuth" /> class.
        /// </summary>
        /// <param name="sexagesimal">The <see cref="Sexagesimal" /> value.</param>
        private Azimuth(Sexagesimal sexagesimal)
        {
            Sexagesimal = sexagesimal;
        }

        /// <summary>
        ///     Returns the <see cref="Sexagesimal" /> value of this azimuth.
        /// </summary>
        public Sexagesimal Sexagesimal { get; }

        /// <summary>
        ///     Converts a string to its <see cref="Azimuth" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Azimuth" /> equivalent of the string.</returns>
        public static Azimuth Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new Azimuth(Sexagesimal.Empty);
            }

            try
            {
                return new Azimuth(Sexagesimal.Parse(value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid azimuth", ex);
            }
        }
    }
}