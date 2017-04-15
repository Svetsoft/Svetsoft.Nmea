using System;

namespace Svetsoft.Geography.Positioning
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

        /// <summary>
        ///     Converts an elevation value to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="Elevation" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParse(string value, out Elevation result)
        {
            result = default(Elevation);
            try
            {
                result = Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}