using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about satellites in view.
    /// </summary>
    public class GpgsvSentence : NmeaSentence
    {
        private readonly List<Satellite> _satellitesInView;

        /// <summary>
        ///     Creates a new instance of the <see cref="GpgsvSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GpgsvSentence(string sentence)
            : base(sentence)
        {
            _satellitesInView = new List<Satellite>();
            Parse();
        }

        /// <summary>
        ///     Returns the list of satellites in view.
        /// </summary>
        public ReadOnlyCollection<Satellite> SatellitesInView
        {
            get { return new ReadOnlyCollection<Satellite>(_satellitesInView); }
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
        public int SatellitesInViewCount { get; internal set; }

        /// <summary>
        ///     Adds a <see cref="Satellite" /> to the end of the list,
        /// </summary>
        /// <param name="satellite">The <see cref="Satellite" /> to be added to the end of the list.</param>
        internal void AddSatelliteInView(Satellite satellite)
        {
            _satellitesInView.Add(satellite);
        }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GpgsvSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;
            if (fields.Length <= 2)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Total number of messages of this type in this cycle
            MessagesCount = int.Parse(fields[0]);

            // Message number
            MessageNumber = int.Parse(fields[1]);

            // Total number of visible satellites
            if (!string.IsNullOrWhiteSpace(fields[2]))
            {
                SatellitesInViewCount = int.Parse(fields[2]);
            }

            // Satellites have an exact number of fields (e.g.: Pseudo-Random Number, Elevation, Azimuth and Serial-To-Noise Ratio)
            if ((fields.Length - (FieldsPerSatelliteCount + FieldsSkippedCount)) % FieldsPerSatelliteCount != 0)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Satellite details
            for (var index = 0; index < MaximumSatellitesPerSentenceCount; index++)
            {
                var currentFieldIndex = index * FieldsPerSatelliteCount + FieldsSkippedCount;

                // Validate that there are more fields to process
                if (currentFieldIndex > fields.Length - 1)
                {
                    break;
                }

                // Parse the PRN that will uniquely identify the satellite
                var pseudoRandomNoise = PseudoRandomNoise.Parse(fields[currentFieldIndex]);

                // Verify that the elevation exits; otherwise use an empty value
                Elevation elevation;
                if (fields.Length > currentFieldIndex + 1 && !string.IsNullOrWhiteSpace(fields[currentFieldIndex + 1]))
                {
                    elevation = Elevation.Parse(fields[currentFieldIndex + 1]);
                }
                else
                {
                    elevation = Elevation.Empty;
                }

                // Verify that the azimuth exists; otherwise use an empty value
                Azimuth azimuth;
                if (fields.Length > currentFieldIndex + 2 && !string.IsNullOrWhiteSpace(fields[currentFieldIndex + 2]))
                {
                    azimuth = Azimuth.ParseAzimuth(fields[currentFieldIndex + 2]);
                }
                else
                {
                    azimuth = Azimuth.Empty;
                }

                // Verify that the SRN exists; otherwise use an empty value
                SignalToNoiseRatio signalToNoiseRatio;
                if (fields.Length > currentFieldIndex + 3 && !string.IsNullOrWhiteSpace(fields[currentFieldIndex + 3]))
                {
                    signalToNoiseRatio = SignalToNoiseRatio.Parse(fields[currentFieldIndex + 3]);
                }
                else
                {
                    signalToNoiseRatio = SignalToNoiseRatio.Empty;
                }

                // Add the satellite
                AddSatelliteInView(new Satellite(pseudoRandomNoise, elevation, azimuth, signalToNoiseRatio));
            }
        }
    }
}