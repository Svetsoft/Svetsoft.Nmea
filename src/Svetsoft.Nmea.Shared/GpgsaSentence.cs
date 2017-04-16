using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
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
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GpgsaSentence(string sentence)
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
        ///     Adds a <see cref="PseudoRandomNoise" /> to the end of the list,
        /// </summary>
        /// <param name="pseudoRandomNoise">The <see cref="PseudoRandomNoise" /> to be added to the end of the list.</param>
        internal void AddSatellite(PseudoRandomNoise pseudoRandomNoise)
        {
            _satellitePrns.Add(pseudoRandomNoise);
        }

        /// <summary>
        ///     Converts a GPGSV sentence to its <see cref="GpgsaSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;
            if (fields.Length < 17)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Fix mode
            if (!string.IsNullOrWhiteSpace(fields[0]))
            {
                FixMode = Fix.ParseMode(fields[0]);
            }

            // Fix plane
            if (!string.IsNullOrWhiteSpace(fields[1]))
            {
                FixPlane = Fix.ParsePlane(fields[1]);
            }

            // Satellite PRN's
            for (var index = 2; index < 14; index++)
            {
                // Skip PRN's that are not provided
                if (string.IsNullOrWhiteSpace(fields[index]))
                {
                    continue;
                }

                // Parse the PRN that uniquely identifies the satellite
                var pseudoRandomNoise = PseudoRandomNoise.Parse(fields[index]);

                // Add the PRN
                AddSatellite(pseudoRandomNoise);
            }

            // Position Dilution of Precision
            if (!string.IsNullOrWhiteSpace(fields[14]))
            {
                PositionDilutionOfPrecision = DilutionOfPrecision.Parse(fields[14]);
            }

            // Horizontal Dilution of Precision
            if (!string.IsNullOrWhiteSpace(fields[15]))
            {
                HorizontalDilutionOfPrecision = DilutionOfPrecision.Parse(fields[15]);
            }

            // Vertical Dilution of Precision
            if (!string.IsNullOrWhiteSpace(fields[16]))
            {
                VerticalDilutionOfPrecision = DilutionOfPrecision.Parse(fields[16]);
            }
        }
    }
}