using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents the additional multiplicative effect of navigation satellite geometry on positional measurement
    ///     precision.
    /// </summary>
    public class DilutionOfPrecision
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DilutionOfPrecision" /> class.
        /// </summary>
        /// <param name="value">The value that represents the dilution of precision.</param>
        public DilutionOfPrecision(float value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Dilution of precision must be bigger than zero", nameof(value));
            }

            Value = value;
            Rating = value.ToDilutionOfPrecisionRating();
        }

        /// <summary>
        ///     Returns the value of this dilution of precision.
        /// </summary>
        public float Value { get; }

        /// <summary>
        ///     Returns the rating of this dilution of precision.
        /// </summary>
        public DilutionOfPrecisionRating Rating { get; }

        /// <summary>
        ///     Converts a string to its <see cref="DilutionOfPrecision" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="DilutionOfPrecision" /> equivalent of the string.</returns>
        public static DilutionOfPrecision Parse(string value)
        {
            try
            {
                return new DilutionOfPrecision(float.Parse(value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid dilution of precision", ex);
            }
        }

        /// <summary>
        ///     Converts an azimuth value to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="DilutionOfPrecision" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParse(string value, out DilutionOfPrecision result)
        {
            result = default(DilutionOfPrecision);
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