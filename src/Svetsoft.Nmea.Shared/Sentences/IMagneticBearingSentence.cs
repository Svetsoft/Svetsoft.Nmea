namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Defines members of a sentence that contains magnetic bearing.
    /// </summary>
    public interface IMagneticBearingSentence
    {
        /// <summary>
        ///     Returns the <see cref="Bearing">MagneticBearing</see>.
        /// </summary>
        Bearing MagneticBearing { get; }
    }
}