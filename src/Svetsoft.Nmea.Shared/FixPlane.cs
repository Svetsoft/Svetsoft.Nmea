namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Indicates whether a position fix is computed in a two-dimensional (2D) or three-dimensional (3D) plane.
    /// </summary>
    public enum FixPlane
    {
        /// <summary>
        ///     The position fix is not available.
        /// </summary>
        NotAvailable,

        /// <summary>
        ///     The position fix is computed in a two-dimensional (2D) plane.
        /// </summary>
        TwoDimensional,

        /// <summary>
        ///     The position fix is computed in a three-dimensional (3D) plane.
        /// </summary>
        ThreeDimensional
    }
}