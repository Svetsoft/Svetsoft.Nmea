using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a point on a route or line of travel.
    /// </summary>
    public class Waypoint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Waypoint" /> class.
        /// </summary>
        /// <param name="name">The name of the waypoint.</param>
        public Waypoint(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Returns the name of this waypoint.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Converts a string to its <see cref="Waypoint" /> equivalent.
        /// </summary>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Waypoint" /> equivalent of the string.</returns>
        public static Waypoint Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FormatException($"{nameof(value)} is not in the correct format");
            }

            return new Waypoint(value);
        }
    }
}