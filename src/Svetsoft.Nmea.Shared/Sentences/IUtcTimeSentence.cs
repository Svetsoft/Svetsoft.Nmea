using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Defines members of a sentence that contains time expressed as the Coordinated Universal Time (UTC).
    /// </summary>
    public interface IUtcTimeSentence
    {
        /// <summary>
        ///     Returns the time of day, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        TimeSpan UtcTime { get; }
    }
}