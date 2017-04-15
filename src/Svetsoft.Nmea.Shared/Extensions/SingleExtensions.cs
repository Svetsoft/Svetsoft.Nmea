using System;

namespace Svetsoft.Geography.Positioning
{
    internal static class SingleExtensions
    {
        /// <summary>
        ///     Converts the numeric value of this instance to its equivalent rating of dilution of precision.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The <see cref="DilutionOfPrecisionRating" /> equivalent of the numeric value.</returns>
        public static DilutionOfPrecisionRating ToDilutionOfPrecisionRating(this float value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Dilution of precision must be bigger than zero", nameof(value));
            }

            // Ideal: < 1
            if (value > 0.0f && value <= 1.0f)
            {
                return DilutionOfPrecisionRating.Ideal;
            }
            // Excellent: 1-2
            if (value <= 2.0f)
            {
                return DilutionOfPrecisionRating.Excellent;
            }

            // Good: 2-5
            if (value <= 5.0f)
            {
                return DilutionOfPrecisionRating.Good;
            }

            // Moderate: 5-8
            if (value <= 8.0f)
            {
                return DilutionOfPrecisionRating.Moderate;
            }

            // Fair: 10-20
            if (value <= 20.0f)
            {
                return DilutionOfPrecisionRating.Fair;
            }

            // Poor: > 20
            return DilutionOfPrecisionRating.Poor;
        }
    }
}