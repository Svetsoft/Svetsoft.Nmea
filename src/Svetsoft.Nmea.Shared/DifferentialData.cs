using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Returns the data related to differential GPS.
    /// </summary>
    public class DifferentialData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DifferentialData"/> class.
        /// </summary>
        /// <param name="secondsSinceLastDifferentialGpsSc104Update">The age of differential GPS data described in seconds since the last SC104 (type 1 or 9) update.</param>
        /// <param name="differentialGpsReferenceStationId">The differential reference station ID.</param>
        public DifferentialData(TimeSpan secondsSinceLastDifferentialGpsSc104Update, int differentialGpsReferenceStationId)
        {
            SecondsSinceLastDifferentialGpsSc104Update = secondsSinceLastDifferentialGpsSc104Update;
            DifferentialGpsReferenceStationId = differentialGpsReferenceStationId;
        }

        /// <summary>
        ///     Returns the age of differential GPS data described in seconds since the last SC104 (type 1 or 9) update.
        /// </summary>
        public TimeSpan SecondsSinceLastDifferentialGpsSc104Update { get; }

        /// <summary>
        ///     Returns the differential reference station ID.
        /// </summary>
        public int DifferentialGpsReferenceStationId { get; }
    }
}