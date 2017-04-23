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
    }
}