namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Defines members of a sentence that contains waypoint.
    /// </summary>
    public interface IWaypointSentence
    {
        /// <summary>
        ///     Returns the <see cref="Waypoint" />.
        /// </summary>
        Waypoint Waypoint { get; }
    }
}