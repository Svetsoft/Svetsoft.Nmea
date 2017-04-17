namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Indicates the unit of measure for distance measurements.
    /// </summary>
    public enum DistanceUnit
    {
        /// <summary>
        ///     Base unit of length in the International System of Units.
        /// </summary>
        Meters,

        /// <summary>
        ///     Unit of length also known as "Sea miles".
        /// </summary>
        NauticalMiles,

        /// <summary>
        ///     Unit of length in the International System of Units, equal to 1000 meters.
        /// </summary>
        Kilometers
    }
}