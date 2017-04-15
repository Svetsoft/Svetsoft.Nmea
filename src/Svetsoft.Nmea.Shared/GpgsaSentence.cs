using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a GPGSA sentence of the NMEA specification with details about dilution of precision and active
    ///     satellites.
    /// </summary>
    public class GpgsaSentence : NmeaSentence
    {
        private readonly List<PseudoRandomNoise> _satellitePrns;

        /// <summary>
        ///     Creates a new instance of the <see cref="GpgsaSentence" /> class.
        /// </summary>
        /// <param name="nmeaSentence">The <see cref="NmeaSentence" /> to copy values from.</param>
        internal GpgsaSentence(NmeaSentence nmeaSentence)
            : base(nmeaSentence)
        {
            _satellitePrns = new List<PseudoRandomNoise>();
        }

        /// <summary>
        ///     Returns the list of satellites PRN's used in position fix.
        /// </summary>
        public ReadOnlyCollection<PseudoRandomNoise> SatellitePrns
        {
            get { return new ReadOnlyCollection<PseudoRandomNoise>(_satellitePrns); }
        }

        /// <summary>
        ///     Returns the <see cref="FixMode" /> of this sentence.
        /// </summary>
        public FixMode FixMode { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="FixPlane" /> of this sentence.
        /// </summary>
        public FixPlane FixPlane { get; internal set; }

        /// <summary>
        ///     Returns the <see cref="DilutionOfPrecision" /> of the position in the three-dimensional (3D) plane in this cycle.
        /// </summary>
        public DilutionOfPrecision PositionDilutionOfPrecision { get; internal set; }

        /// <summary>
        ///     Returns the horizontal <see cref="DilutionOfPrecision" />.
        /// </summary>
        public DilutionOfPrecision HorizontalDilutionOfPrecision { get; internal set; }

        /// <summary>
        ///     Returns the vertical <see cref="DilutionOfPrecision" />.
        /// </summary>
        public DilutionOfPrecision VerticalDilutionOfPrecision { get; internal set; }

        /// <summary>
        ///     Adds a <see cref="PseudoRandomNoise" /> to the end of the list,
        /// </summary>
        /// <param name="pseudoRandomNoise">The <see cref="PseudoRandomNoise" /> to be added to the end of the list.</param>
        internal void AddSatellite(PseudoRandomNoise pseudoRandomNoise)
        {
            _satellitePrns.Add(pseudoRandomNoise);
        }
    }
}