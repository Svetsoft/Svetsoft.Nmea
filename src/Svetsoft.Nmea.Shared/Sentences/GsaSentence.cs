using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for dilution of precision and active satellites.
    /// </summary>
    public class GsaSentence : NmeaSentence
    {
        private readonly List<PseudoRandomNoise> _satellitePrns;

        /// <summary>
        ///     Creates a new instance of the <see cref="GsaSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GsaSentence(string sentence)
            : base(sentence)
        {
            _satellitePrns = new List<PseudoRandomNoise>();
            Parse();
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
        ///     Adds <see cref="PseudoRandomNoise" /> elements of the specified collection to the end of the list.
        /// </summary>
        /// <param name="collection">The collection whose <see cref="PseudoRandomNoise" /> elements should be added to the end of the list.</param>
        internal void AddSatelliteRange(IEnumerable<PseudoRandomNoise> collection)
        {
            _satellitePrns.AddRange(collection);
        }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GsaSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;
            if (fields.Length < 17)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            FixMode = GetFixMode(0);
            FixPlane = GetFixPlane(1);
            AddSatelliteRange(GetPseudoRandomNoise(2, 14));
            PositionDilutionOfPrecision = GetDilutionOfPrecision(14);
            HorizontalDilutionOfPrecision = GetDilutionOfPrecision(15);
            VerticalDilutionOfPrecision = GetDilutionOfPrecision(16);
        }
    }
}