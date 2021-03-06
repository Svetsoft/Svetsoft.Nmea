﻿using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for geographic position (latitude/longitude)
    ///     and time.
    /// </summary>
    public class GllSentence : NmeaSentence
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="GllSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GllSentence(string sentence)
            : base(sentence)
        {
            Parse();
        }

        /// <summary>
        ///     Returns the time of day of this position, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Returns whether this is a position fix.
        /// </summary>
        public Status IsFix { get; internal set; }

        /// <summary>
        ///     Returns the position in this cycle.
        /// </summary>
        public Position Position { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GllSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            Position = GetPosition(0);
            UtcTime = GetUtcTime(4);
            IsFix = GetStatus(5);
        }
    }
}