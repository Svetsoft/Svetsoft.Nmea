using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Svetsoft.Nmea.Extensions;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification.
    /// </summary>
    public abstract class NmeaSentence
    {
        protected const char TimeSpanMillisecondsDelimiter = '.';
        protected const char StartDelimiter = '$';
        protected const char FieldDelimiter = ',';
        protected const char ChecksumDelimiter = '*';
        protected const int ChecksumLength = 2;

        /// <summary>
        ///     Returns the value represents the base to calculate GPS date and time.
        /// </summary>
        public readonly DateTime DateTimeBase = new DateTime(1980, 01, 06);

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence to create the instance from.</param>
        protected NmeaSentence(string sentence)
        {
            Parse(sentence);
        }

        /// <summary>
        ///     Returns the fields in this sentence.
        /// </summary>
        public string[] Fields { get; internal set; }

        /// <summary>
        ///     Returns the checksum of this sentence.
        /// </summary>
        public string Checksum { get; internal set; }

        /// <summary>
        ///     Returns a copy of this sentence.
        /// </summary>
        public string Sentence { get; internal set; }

        /// <summary>
        ///     Returns the type of message contained in this sentence.
        /// </summary>
        public string MessageType { get; internal set; }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="SteeringDirection" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected SteeringDirection GetSteeringDirection(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? SteeringDirection.Parse(Fields[index]) : default(SteeringDirection);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="FixQuality" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected FixQuality GetFixQuality(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? FixQuality.Parse(Fields[index]) : default(FixQuality);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="DilutionOfPrecision" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected DilutionOfPrecision GetDilutionOfPrecision(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? DilutionOfPrecision.Parse(Fields[index]) : default(DilutionOfPrecision);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Status" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Status GetStatus(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Status.Parse(Fields[index]) : default(Status);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Bearing" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Bearing GetBearing(int index)
        {
            return Fields.Length > index + 1 && Fields.All(Enumerable.Range(index, 2), s => !string.IsNullOrWhiteSpace(s)) ? Bearing.Parse(Fields.ToArray(index, 2)) : default(Bearing);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Speed" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Speed GetSpeed(int index)
        {
            return Fields.Length > index + 1 && Fields.All(Enumerable.Range(index, 2), s => !string.IsNullOrWhiteSpace(s)) ? Speed.Parse(Fields.ToArray(index, 2)) : default(Speed);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Speed" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <param name="unit">The unit in which the speed is measured.</param>
        /// <returns>The value of the specified index.</returns>
        protected Speed GetSpeed(int index, SpeedUnit unit)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Speed.Parse(unit, Fields[index]) : default(Speed);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Position" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Position GetPosition(int index)
        {
            return Fields.Length > index + 3 && Fields.All(Enumerable.Range(index, 4), s => !string.IsNullOrWhiteSpace(s)) ? Position.Parse(Fields.ToArray(index, 4)) : default(Position);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="TimeSpan" /> object, expressed as the Coordinated Universal
        ///     Time (UTC).
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index, expressed as the Coordinated Universal Time (UTC).</returns>
        protected TimeSpan GetUtcTime(int index)
        {
            if (Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]))
            {
                var utcString = Fields[index];
                var utcHours = int.Parse(utcString.Substring(0, 2));
                var utcMinutes = int.Parse(utcString.Substring(2, 2));
                var utcSeconds = int.Parse(utcString.Substring(4, 2));
                if (utcString.Contains(TimeSpanMillisecondsDelimiter, out int utcMillisecondsIndex))
                {
                    var utcMilliseconds = int.Parse(utcString.Substring(utcMillisecondsIndex + 1, utcString.Length - (utcMillisecondsIndex + 1))) * 1000;
                    return new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
                }

                return new TimeSpan(utcHours, utcMinutes, utcSeconds);
            }

            return default(TimeSpan);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="DateTime" /> object, expressed as the Coordinated Universal
        ///     Time (UTC).
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index, expressed as the Coordinated Universal Time (UTC).</returns>
        protected DateTime GetUtcDate(int index)
        {
            if (Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]))
            {
                var utcDateString = Fields[index];
                var utcDay = int.Parse(utcDateString.Substring(0, 2));
                var utcMonth = int.Parse(utcDateString.Substring(2, 2));
                var utcYear = int.Parse(utcDateString.Substring(4, 2));
                return new DateTime(2000 + utcYear, utcMonth, utcDay);
            }

            return default(DateTime);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Double" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected double GetDouble(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? double.Parse(Fields[index]) : default(double);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Int32" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected int GetInt32(int index)
        {
            return GetInt32(index, NumberStyles.Integer);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Int32" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <param name="style">
        ///     A bitwise combination of the enumeration values that indicates the style elements that can be
        ///     present.
        /// </param>
        /// <returns>The value of the specified index.</returns>
        protected int GetInt32(int index, NumberStyles style)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? int.Parse(Fields[index], style) : default(int);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="DistanceUnit" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected DistanceUnit GetDistanceUnit(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? DistanceUnit.Parse(Fields[index]) : default(DistanceUnit);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="FixMode" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected FixMode GetFixMode(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? FixMode.Parse(Fields[index]) : default(FixMode);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="FixPlane" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected FixPlane GetFixPlane(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? FixPlane.Parse(Fields[index]) : default(FixPlane);
        }

        /// <summary>
        ///     Gets the value of the specified field as a Datum name.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected string GetDatumName(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Fields[index] : default(string);
        }

        /// <summary>
        ///     Gets the value of the specified field as a Datum code.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected string GetDatumCode(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Fields[index] : default(string);
        }

        /// <summary>
        ///     Gets the value of the specified field as a Datum subcode.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected string GetDatumSubCode(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Fields[index] : default(string);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Distance" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Distance GetDistance(int index)
        {
            return Fields.Length > index + 1 && Fields.All(Enumerable.Range(index, 2), s => !string.IsNullOrWhiteSpace(s)) ? Distance.Parse(Fields.ToArray(index, 2)) : default(Distance);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Distance" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <param name="unit">The <see cref="DistanceUnit" /> in which the distance is represented.</param>
        /// <returns>The value of the specified index.</returns>
        protected Distance GetDistance(int index, DistanceUnit unit)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Distance.Parse(unit, Fields[index]) : default(Distance);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Longitude" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Longitude GetLongitude(int index)
        {
            return Fields.Length > index + 1 && Fields.All(Enumerable.Range(index, 2), s => !string.IsNullOrWhiteSpace(s)) ? new Longitude(Sexagesimal.Parse(Fields[index]), LongitudeHemisphere.Parse(Fields[index + 1])) : default(Longitude);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="DifferentialData" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected DifferentialData GetDifferentialData(int index)
        {
            // Verify that the differential GPS exists; otherwise use an empty value
            if (Fields.Length >= index + 1 && !string.IsNullOrWhiteSpace(Fields[index]) && !string.IsNullOrWhiteSpace(Fields[index + 1]))
            {
                return new DifferentialData(TimeSpan.FromSeconds(float.Parse(Fields[index])), int.Parse(Fields[index + 1]));
            }

            return null;
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="PseudoRandomNoise" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected PseudoRandomNoise GetPseudoRandomNoise(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? PseudoRandomNoise.Parse(Fields[index]) : default(PseudoRandomNoise);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Satellite" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Satellite GetSatelliteInView(int index)
        {
            var pseudoRandomNoise = GetPseudoRandomNoise(index);
            var elevation = GetElevation(index + 1);
            var azimuth = GetAzimuth(index + 2);
            var signalToNoiseRatio = GetSignalToNoiseRatio(index + 3);

            return new Satellite(pseudoRandomNoise, elevation, azimuth, signalToNoiseRatio);
        }

        /// <summary>
        ///     Gets a collection of values for a specified index as <see cref="PseudoRandomNoise" /> elemets.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>The collection of <see cref="PseudoRandomNoise" /> elements for the specified range.</returns>
        protected IEnumerable<PseudoRandomNoise> GetPseudoRandomNoise(int index, int count)
        {
            while (index < count)
            {
                // Skip PRN's that are not provided
                if (string.IsNullOrWhiteSpace(Fields[index]))
                {
                    continue;
                }

                yield return GetPseudoRandomNoise(index++);
            }
        }

        /// <summary>
        ///     Gets a collection of values for a specified index as <see cref="Satellite" /> elements.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>The collection of <see cref="Satellite" /> elements for the specified range.</returns>
        protected IEnumerable<Satellite> GetSatellitesInView(int index, int count)
        {
            while (index < count)
            {
                var currentFieldIndex = index * 4;
                if (currentFieldIndex > Fields.Length - 1)
                {
                    break;
                }

                if (string.IsNullOrWhiteSpace(Fields[index]))
                {
                    continue;
                }

                yield return GetSatelliteInView(index++);
            }
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Waypoint" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Waypoint GetWaypoint(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Waypoint.Parse(Fields[index]) : default(Waypoint);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Elevation" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Elevation GetElevation(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Elevation.Parse(Fields[index]) : default(Elevation);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="Azimuth" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected Azimuth GetAzimuth(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? Azimuth.Parse(Fields[index]) : default(Azimuth);
        }

        /// <summary>
        ///     Gets the value of the specified field as a <see cref="SignalToNoiseRatio" /> object.
        /// </summary>
        /// <param name="index">The zero-based index of the field.</param>
        /// <returns>The value of the specified index.</returns>
        protected SignalToNoiseRatio GetSignalToNoiseRatio(int index)
        {
            return Fields.Length > index && !string.IsNullOrWhiteSpace(Fields[index]) ? SignalToNoiseRatio.Parse(Fields[index]) : default(SignalToNoiseRatio);
        }

        /// <summary>
        ///     Computes the checksum value for the specified sentence.
        /// </summary>
        /// <param name="sentence">A string containing the NMEA sentence to compute the checksum for.</param>
        /// <param name="startDelimiterIndex">The index of the start delimiter.</param>
        /// <param name="endDelimiterIndex">The index of the end delimiter.</param>
        /// <returns>The computed checksum.</returns>
        public static byte ComputeChecksum(string sentence, int startDelimiterIndex, int endDelimiterIndex)
        {
            var checksum = Convert.ToByte(sentence[startDelimiterIndex + 1]);
            for (var index = startDelimiterIndex + 2; index < endDelimiterIndex; index++)
            {
                checksum ^= Convert.ToByte(sentence[index]);
            }

            return checksum;
        }

        /// <summary>
        ///     Converts a NMEA sentence to its <see cref="NmeaSentence" /> equivalent.
        /// </summary>
        private void Parse(string sentence)
        {
            // Every NMEA sentence must contains a start delimiter
            if (!sentence.Contains(StartDelimiter, out int startDelimiterIndex))
            {
                throw new FormatException($"The sentence does not contain a \"{StartDelimiter}\" character required by NMEA specification.");
            }

            // Index of the first field delimiter
            if (!sentence.Contains(FieldDelimiter, out int firstFieldDelimiterIndex))
            {
                throw new FormatException($"The sentence does not contain \"{FieldDelimiter}\" character required by NMEA specification.");
            }

            // The start field delimiter must exist before the first field delimiter
            if (firstFieldDelimiterIndex < startDelimiterIndex)
            {
                throw new FormatException("Invalid NMEA sentence format");
            }

            // The characters between start delimiter and first field delimiter represent the message type
            var startMessageTypeIndex = startDelimiterIndex + 1;
            var messageType = sentence.Substring(startMessageTypeIndex, firstFieldDelimiterIndex - startMessageTypeIndex);

            // Data ends in the last character unless a checksum delimiter is detected
            var endDelimiterIndex = sentence.Length;
            if (sentence.Contains(ChecksumDelimiter, out int checksumDelimiterIndex))
            {
                endDelimiterIndex = checksumDelimiterIndex;
            }
            var dataEndIndex = endDelimiterIndex - 1;

            // Data starts right after the first field delimiter
            var dataStartIndex = firstFieldDelimiterIndex + 1;

            // The index for data start must be before data end index
            if (dataEndIndex < dataStartIndex)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Extract data from sentence
            var data = sentence.Substring(dataStartIndex, dataEndIndex - dataStartIndex + 1);
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Split data with the field delimiter
            var fields = data.Split(FieldDelimiter);
            if (fields.Length == 0)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Compute the checksum for the sentence
            var computedChecksum = ComputeChecksum(sentence, startDelimiterIndex, endDelimiterIndex)
                .ToString("X2");

            // If a checksum is provided then both must match
            if (checksumDelimiterIndex != -1 && sentence.Length >= checksumDelimiterIndex + 1 + ChecksumLength)
            {
                var checksum = sentence.Substring(checksumDelimiterIndex + 1, ChecksumLength);
                if (!checksum.Equals(computedChecksum, StringComparison.Ordinal))
                {
                    // Checksums don't match
                    throw new ChecksumMismatchException("Checksum in sentence does not match computed checksum.");
                }
            }

            Sentence = sentence;
            MessageType = messageType;
            Fields = fields;
            Checksum = computedChecksum;
        }
    }
}