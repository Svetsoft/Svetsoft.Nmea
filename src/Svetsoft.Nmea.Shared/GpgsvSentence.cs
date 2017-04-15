using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a GPGSV sentence of the NMEA specification with details about visible satellites, including their
    ///     signal-to-noise ratio.
    /// </summary>
    public class GpgsvSentence : NmeaSentence
    {
        private readonly List<Satellite> _satellites;

        /// <summary>
        ///     Creates a new instance of the <see cref="GpgsvSentence" /> class.
        /// </summary>
        /// <param name="nmeaSentence">The <see cref="NmeaSentence" /> to copy values from.</param>
        internal GpgsvSentence(NmeaSentence nmeaSentence)
            : base(nmeaSentence)
        {
            _satellites = new List<Satellite>();
        }

        /// <summary>
        ///     Returns the list of visible satellites.
        /// </summary>
        public ReadOnlyCollection<Satellite> Satellites
        {
            get { return new ReadOnlyCollection<Satellite>(_satellites); }
        }

        /// <summary>
        ///     Returns the total number of messages of this type in this cycle.
        /// </summary>
        public int MessagesCount { get; internal set; }

        /// <summary>
        ///     Returns the message number associated with this sentence.
        /// </summary>
        public int MessageNumber { get; internal set; }

        /// <summary>
        ///     Returns the number of visible satellites in this cycle.
        /// </summary>
        public int VisibleSatellitesCount { get; internal set; }

        /// <summary>
        ///     Adds a <see cref="Satellite" /> to the end of the list,
        /// </summary>
        /// <param name="satellite">The <see cref="Satellite" /> to be added to the end of the list.</param>
        internal void AddSatellite(Satellite satellite)
        {
            _satellites.Add(satellite);
        }
    }
}