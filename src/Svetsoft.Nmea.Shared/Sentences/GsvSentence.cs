using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for satellites in view.
    /// </summary>
    public class GsvSentence : NmeaSentence
    {
        protected const int MaxSatellitesPerSentence = 6;
        protected const int FieldsPerSatelliteCount = 4;
        private readonly List<Satellite> _satellitesInView;

        /// <summary>
        ///     Creates a new instance of the <see cref="GsvSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GsvSentence(string sentence)
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
        ///     Adds <see cref="Satellite" /> elements of the specified collection to the end of the list.
        /// </summary>
        /// <param name="collection">The collection whose <see cref="Satellite" /> elements should be added to the end of the list.</param>
        internal void AddSatellitesInViewRange(IEnumerable<Satellite> collection)
        {
            _satellitesInView.AddRange(collection);
        }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GsvSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            var fields = Fields;
            if (fields.Length <= 2)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            MessagesCount = GetInt32(0);
            MessageNumber = GetInt32(1);
            SatellitesInViewCount = GetInt32(2);

            // Satellites have an exact number of fields (e.g.: Pseudo-Random Number, Elevation, Azimuth and Serial-To-Noise Ratio)
            if ((fields.Length - (FieldsPerSatelliteCount + 3)) % FieldsPerSatelliteCount != 0)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            AddSatellitesInViewRange(GetSatellitesInView(3, MaxSatellitesPerSentence));
        }
    }
}