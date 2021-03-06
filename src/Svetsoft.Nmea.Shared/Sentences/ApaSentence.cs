﻿using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for Autopilot (A).
    /// </summary>
    public sealed class ApaSentence : AutoPilotSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ApaSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        [Obsolete("Autopilot (A) sentence (APA) has been designated obsolete by NMEA in 2009.")]
        public ApaSentence(string sentence)
            : base(sentence)
        {
            throw new NotImplementedException($"{nameof(ApaSentence)} is not implemented");
        }
    }
}