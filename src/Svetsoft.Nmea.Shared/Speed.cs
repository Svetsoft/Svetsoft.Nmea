namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a rate at which an object moves.
    /// </summary>
    public class Speed
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Speed" /> class.
        /// </summary>
        /// <param name="unit">The <see cref="SpeedUnit" /> in which the speed is represented.</param>
        /// <param name="value">The value that the speed represents.</param>
        public Speed(SpeedUnit unit, double value)
        {
            Unit = unit;
            Value = value;
        }

        /// <summary>
        ///     The value that this speed represents.
        /// </summary>
        public double Value { get; }

        /// <summary>
        ///     Returns the <see cref="SpeedUnit" /> in which this speed is represented.
        /// </summary>
        public SpeedUnit Unit { get; }

        /// <summary>
        ///     Converts a string to its <see cref="Speed" /> equivalent.
        /// </summary>
        /// <param name="unit">A <see cref="SpeedUnit" /> in which <paramref name="value" /> is represented.</param>
        /// <param name="value">A string containing a value to convert.</param>
        /// <returns>The <see cref="Speed" /> equivalent of the string.</returns>
        public static Speed Parse(SpeedUnit unit, string value)
        {
            return new Speed(unit, double.Parse(value));
        }

        /// <summary>
        ///     Converts a speed value to its managed equivalent. A return value indicates whether the conversion
        ///     succeeded.
        /// </summary>
        /// <param name="unit">A <see cref="SpeedUnit" /> in which <paramref name="value" /> is represented.</param>
        /// <param name="value">A string containing a value to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="Speed" /> equivalent of the message
        ///     contained in <paramref name="value" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="value" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="value" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParse(SpeedUnit unit, string value, out Speed result)
        {
            result = default(Speed);
            try
            {
                result = Parse(unit, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}