using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a course along an object moves.
    /// </summary>
    public class Direction
    {
        private const char SteerDirectionLeftDelimiter = 'L';
        private const char SteerDirectionRightDelimiter = 'R';

        /// <summary>
        ///     Converts a string to its <see cref="SteeringDirection" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="SteeringDirection" /> equivalent of the string.</returns>
        public static SteeringDirection ParseSteeringDirection(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException("Invalid steering direction format");
            }

            if (value.Contains(SteerDirectionLeftDelimiter))
            {
                return SteeringDirection.Left;
            }

            if (value.Contains(SteerDirectionRightDelimiter))
            {
                return SteeringDirection.Right;
            }

            throw new FormatException("Invalid steering direction format");
        }
    }
}