namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Defines members of a sentence that contains true bearing.
    /// </summary>
    public interface ITrueBearingSentence
    {
        /// <summary>
        ///     Returns the <see cref="Bearing">TrueBearing</see>.
        /// </summary>
        Bearing TrueBearing { get; }
    }
}