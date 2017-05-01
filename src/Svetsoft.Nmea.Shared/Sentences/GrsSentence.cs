using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification for GPS range residuals.
    /// </summary>
    public class GrsSentence : NmeaSentence, IUtcTimeSentence
    {
        private readonly List<Distance> _satelliteResiduals;

        /// <summary>
        ///     Creates a new instance of the <see cref="GrsSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        public GrsSentence(string sentence)
            : base(sentence)
        {
            _satelliteResiduals = new List<Distance>();
            Parse();
        }

        /// <summary>
        ///     Returns the list of satellite residuals.
        /// </summary>
        public ReadOnlyCollection<Distance> SatelliteResiduals
        {
            get { return new ReadOnlyCollection<Distance>(_satelliteResiduals); }
        }

        /// <summary>
        ///     Returns the type of residual.
        /// </summary>
        public ResidualType ResidualType { get; internal set; }

        /// <summary>
        ///     Returns the time of day, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        public TimeSpan UtcTime { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="GrsSentence" /> equivalent.
        /// </summary>
        private void Parse()
        {
            UtcTime = GetUtcTime(0);
            ResidualType = GetResidualType(1);
            AddSatelliteResidualRange(GetDistances(2, 12, DistanceUnit.Meters));
        }

        /// <summary>
        ///     Adds <see cref="Distance" /> elements of the specified collection to the end of the list.
        /// </summary>
        /// <param name="collection">The collection whose <see cref="Distance" /> elements should be added to the end of the list.</param>
        internal void AddSatelliteResidualRange(IEnumerable<Distance> collection)
        {
            _satelliteResiduals.AddRange(collection);
        }
    }
}