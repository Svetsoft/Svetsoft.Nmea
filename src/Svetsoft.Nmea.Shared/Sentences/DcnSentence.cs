using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for Decca Position.
    /// </summary>
    public sealed class DcnSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="DcnSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        [Obsolete("Decca Position sentence (DCN) has been designated obsolete by NMEA as of 3.01.")]
        public DcnSentence(string sentence)
            : base(sentence)
        {
            throw new NotImplementedException($"{nameof(DcnSentence)} is not implemented");
        }
    }
}