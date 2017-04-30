using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for geographic position (Loran-C).
    /// </summary>
    public sealed class GlcSentence : AutoPilotSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GlcSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        [Obsolete("Geographic position (Loran-C) sentence (GLC) has been designated obsolete by NMEA in 2010 with last operations shut down in 2016.")]
        public GlcSentence(string sentence)
            : base(sentence)
        {
            throw new NotImplementedException($"{nameof(GlcSentence)} is not implemented");
        }
    }
}