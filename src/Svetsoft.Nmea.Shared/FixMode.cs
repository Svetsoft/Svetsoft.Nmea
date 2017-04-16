namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Indicates whether a position fix is automatically computed in a two-dimensional (2D) or three-dimensional (3D)
    ///     plane or requires manual evaluation.
    /// </summary>
    public enum FixMode
    {
        /// <summary>
        ///     The position fix is automatically computed in a two-dimensional (2D) or three-dimensional (3D) plane.
        /// </summary>
        Automatic,

        /// <summary>
        ///     The position fix requires manual evaluation.
        /// </summary>
        Manual
    }
}