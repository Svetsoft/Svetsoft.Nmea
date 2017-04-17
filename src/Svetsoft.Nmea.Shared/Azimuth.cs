using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents an angular measurement between projected and reference vectors on a spherical coordinate system.
    /// </summary>
    public class Azimuth
    {
        private const char AbsoluteBearingMagneticDelimiter = 'M';
        private const char AbsoluteBearingTrueDelimiter = 'T';

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
        ///     Converts a string to its <see cref="AbsoluteBearing" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="AbsoluteBearing" /> equivalent of the string.</returns>
        public static AbsoluteBearing ParseAbsoluteBearing(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid absolute bearing format");
            }

            if (value.Contains(AbsoluteBearingMagneticDelimiter))
            {
                return AbsoluteBearing.Magnetic;
            }

            if (value.Contains(AbsoluteBearingTrueDelimiter))
            {
                return AbsoluteBearing.True;
            }

            throw new FormatException("Invalid absolute bearing format");
        }

        /// <summary>
        ///     Converts a string to its <see cref="Azimuth" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Azimuth" /> equivalent of the string.</returns>
        public static Azimuth ParseAzimuth(string value)
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

        /// <summary>
        ///     Converts an azimuth value to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="Azimuth" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParse(string value, out Azimuth result)
        {
            result = default(Azimuth);
            try
            {
                result = ParseAzimuth(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}