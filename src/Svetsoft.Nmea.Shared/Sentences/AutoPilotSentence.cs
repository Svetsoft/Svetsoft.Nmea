﻿using System;
using System.Linq;
using Svetsoft.Nmea.Extensions;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification about Autopilot.
    /// </summary>
    public abstract class AutoPilotSentence : NmeaSentence
    {
        protected const string LoranCActiveDelimiter = "V";
        protected const string TrueString = "A";

        /// <summary>
        ///     Creates a new instance of the <see cref="AutoPilotSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        protected AutoPilotSentence(string sentence)
            : base(sentence)
        {
        }

        /// <summary>
        ///     Returns whether the arrival circle is entered.
        /// </summary>
        public bool IsArrivalCircleEntered { get; internal set; }

        /// <summary>
        ///     Returns whether the waypoint is passed perpendicularly.
        /// </summary>
        public bool IsPerpendicularPassedAtWaypoint { get; internal set; }

        /// <summary>
        ///     Returns the name of the destination waypoint.
        /// </summary>
        public string DestinationWaypointId { get; internal set; }

        /// <summary>
        ///     Returns the bearing of origin to destination.
        /// </summary>
        public Bearing BearingOriginToDestination { get; internal set; }

        /// <summary>
        ///     Returns the units of cross-track.
        /// </summary>
        public DistanceUnit CrossTrackUnits { get; internal set; }

        /// <summary>
        ///     Returns the steering direction in this cycle.
        /// </summary>
        public SteeringDirection SteeringDirection { get; internal set; }

        /// <summary>
        ///     Returns the error magnitude of cross-track.
        /// </summary>
        public double CrossTrackErrorMagnitude { get; internal set; }

        /// <summary>
        ///     Returns whether a Loran-C Cycle Lock device is used.
        /// </summary>
        public bool IsLoranCCycleLockActive { get; internal set; }

        /// <summary>
        ///     Returns whether a Loran-C Blink device is used.
        /// </summary>
        public bool IsLoranCBlinkActive { get; internal set; }

        /// <summary>
        ///     Parses the fields of this sentence to its <see cref="AutoPilotSentence" /> equivalent.
        /// </summary>
        protected virtual void Parse()
        {
            var fields = Fields;

            // Loran-C Blink
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                IsLoranCBlinkActive = string.Equals(fields[0], LoranCActiveDelimiter, StringComparison.Ordinal);
            }

            // Loran-C Cycle Lock
            if (fields.Length > 1 && !string.IsNullOrWhiteSpace(fields[1]))
            {
                IsLoranCCycleLockActive = string.Equals(fields[0], LoranCActiveDelimiter, StringComparison.Ordinal);
            }

            // Cross-track error magnitude
            if (fields.Length > 2 && !string.IsNullOrWhiteSpace(fields[2]))
            {
                CrossTrackErrorMagnitude = double.Parse(fields[2]);
            }

            // Direction to steer
            if (fields.Length > 3 && !string.IsNullOrWhiteSpace(fields[3]))
            {
                SteeringDirection = Direction.ParseSteeringDirection(fields[3]);
            }

            // Cross-track units
            if (fields.Length > 4 && !string.IsNullOrWhiteSpace(fields[4]))
            {
                CrossTrackUnits = Distance.ParseUnit(fields[4]);
            }

            // Arrival circle entered
            if (fields.Length > 5 && !string.IsNullOrWhiteSpace(fields[5]))
            {
                IsArrivalCircleEntered = string.Equals(fields[5], TrueString, StringComparison.Ordinal);
            }

            // Perpendicular passed at waypoint
            if (fields.Length > 6 && !string.IsNullOrWhiteSpace(fields[6]))
            {
                IsPerpendicularPassedAtWaypoint = string.Equals(fields[6], TrueString, StringComparison.Ordinal);
            }

            // Bearing origin to destination
            if (fields.Length > 8 && fields.All(Enumerable.Range(7, 2), s => !string.IsNullOrWhiteSpace(s)))
            {
                BearingOriginToDestination = Bearing.Parse(fields.ToArray(7, 2));
            }

            // Destination waypoint ID
            if (fields.Length > 9 && !string.IsNullOrWhiteSpace(fields[9]))
            {
                DestinationWaypointId = fields[9];
            }
        }
    }
}