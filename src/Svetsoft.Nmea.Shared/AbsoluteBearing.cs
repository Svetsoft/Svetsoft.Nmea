namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Indicates whether an absolute navigation refers to magnetic or true bearing.
    /// </summary>
    public enum AbsoluteBearing
    {
        /// <summary>
        ///     The navigation is measured in relation to magnetic north.
        /// </summary>
        Magnetic,

        /// <summary>
        ///     The navigation is measured in relation to the fixed horizontal reference place of true north.
        /// </summary>
        True
    }
}