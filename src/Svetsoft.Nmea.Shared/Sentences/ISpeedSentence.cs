namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Defines members of a sentence that contains speed.
    /// </summary>
    public interface ISpeedSentence
    {
        /// <summary>
        ///     Returns the <see cref="Speed" /> in this cycle.
        /// </summary>
        Speed Speed { get; }
    }
}