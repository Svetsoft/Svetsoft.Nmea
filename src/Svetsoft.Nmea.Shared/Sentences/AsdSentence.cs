using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for Autopilot System Data.
    /// </summary>
    public class AsdSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="AsdSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public AsdSentence(string sentence)
            : base(sentence)
        {
            throw new NotImplementedException($"{nameof(AsdSentence)} is not implemented");
        }
    }
}