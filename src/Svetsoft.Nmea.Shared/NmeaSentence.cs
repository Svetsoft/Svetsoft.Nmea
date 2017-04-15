using System;
using System.Linq;
using Svetsoft.Geography.Positioning.Extensions;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents a sentence of the NMEA specification.
    /// </summary>
    public class NmeaSentence
    {   
        protected const char TimeSpanMillisecondsDelimiter = '.';
        protected const char StartDelimiter = '$';
        protected const char FieldDelimiter = ',';
        protected const char ChecksumDelimiter = '*';
        protected const int ChecksumLength = 2;
        protected const int MaximumSatellitesPerSentenceCount = 6;
        protected const int FieldsPerSatelliteCount = 4;
        protected const int FieldsSkippedCount = 3;

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaSentence" /> class.
        /// </summary>
        /// <param name="sentence">The sentence of the NMEA specification.</param>
        /// <param name="messageType">The type of message contained in the sentence.</param>
        /// <param name="fields">The fields in the sentence.</param>
        /// <param name="checksum">The checksum of the sentence</param>
        internal NmeaSentence(string sentence, string messageType, string[] fields, string checksum)
        {
            Sentence = sentence;
            MessageType = messageType;
            Fields = fields;
            Checksum = checksum;
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaSentence" /> class.
        /// </summary>
        /// <param name="nmeaSentence">The <see cref="NmeaSentence" /> to copy values from.</param>
        internal NmeaSentence(NmeaSentence nmeaSentence)
            : this(nmeaSentence.Sentence, nmeaSentence.MessageType, nmeaSentence.Fields, nmeaSentence.Checksum)
        {
        }

        /// <summary>
        ///     Returns the fields in this sentence.
        /// </summary>
        public string[] Fields { get; }

        /// <summary>
        ///     Returns the checksum of this sentence.
        /// </summary>
        public string Checksum { get; }

        /// <summary>
        ///     Returns a copy of this sentence.
        /// </summary>
        public string Sentence { get; }

        /// <summary>
        ///     Returns the type of message contained in this sentence.
        /// </summary>
        public string MessageType { get; }

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
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <returns>The <see cref="NmeaSentence" /> equivalent of the sentence.</returns>
        public static NmeaSentence ParseBase(string sentence)
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

            return new NmeaSentence(sentence, messageType, fields, computedChecksum);
        }

        /// <summary>
        ///     Converts a GPGGA sentence to its <see cref="GpggaSentence" /> equivalent.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <returns>The <see cref="GpggaSentence" /> equivalent to the sentence.</returns>
        public static GpggaSentence ParseGpgga(string sentence)
        {
            var gpggaSentence = new GpggaSentence(ParseBase(sentence));
            var fields = gpggaSentence.Fields;

            // UTC time of position
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                var utcString = fields[0];
                var utcHours = int.Parse(utcString.Substring(0, 2));
                var utcMinutes = int.Parse(utcString.Substring(2, 2));
                var utcSeconds = int.Parse(utcString.Substring(4, 2));
                if (utcString.Contains(TimeSpanMillisecondsDelimiter, out int utcMillisecondsIndex))
                {
                    var utcMilliseconds = int.Parse(utcString.Substring(utcMillisecondsIndex + 1, utcString.Length - (utcMillisecondsIndex + 1))) * 1000;
                    gpggaSentence.UtcTime = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
                }
                else
                {
                    gpggaSentence.UtcTime = new TimeSpan(utcHours, utcMinutes, utcSeconds);
                }
            }

            // Position (latitude/longitude)
            if (fields.Length > 4 && fields.All(Enumerable.Range(1, 4), s => !string.IsNullOrWhiteSpace(s)))
            {
                gpggaSentence.Position = Position.Parse(fields.ToArray(1, 4));
            }

            // Fix Quality
            if (fields.Length > 5 && !string.IsNullOrWhiteSpace(fields[5]))
            {
                gpggaSentence.FixQuality = Fix.ParseQuality(fields[5]);
            }

            // Total number of visible satellites
            if (fields.Length > 6 && !string.IsNullOrWhiteSpace(fields[6]))
            {
                gpggaSentence.VisibleSatellitesCount = int.Parse(fields[6]);
            }

            // Horizontal Dilution of Precision
            if (fields.Length > 7 && !string.IsNullOrWhiteSpace(fields[7]))
            {
                gpggaSentence.HorizontalDilutionOfPrecision = DilutionOfPrecision.Parse(fields[7]);
            }

            // Altitude
            if (fields.Length > 8 && !string.IsNullOrWhiteSpace(fields[8]) && !string.IsNullOrWhiteSpace(fields[9]))
            {
                gpggaSentence.Altitude = Distance.ParseDistance(Distance.ParseUnit(fields[9]), fields[8]);
            }

            // Geoidal Separator
            if (fields.Length > 9 && !string.IsNullOrWhiteSpace(fields[10]) && !string.IsNullOrWhiteSpace(fields[11]))
            {
                gpggaSentence.GeoidalSeparator = Distance.ParseDistance(Distance.ParseUnit(fields[11]), fields[10]);
            }

            // Verify that the differential GPS exists; otherwise use an empty value
            if (fields.Length >= 14 && !string.IsNullOrWhiteSpace(fields[12]) && !string.IsNullOrWhiteSpace(fields[13]))
            {
                gpggaSentence.SecondsSinceLastDifferentialGpsSc104Update = TimeSpan.FromSeconds(float.Parse(fields[12]));
                gpggaSentence.DifferentialGpsReferenceStationId = int.Parse(fields[13]);
            }
            else
            {
                gpggaSentence.SecondsSinceLastDifferentialGpsSc104Update = null;
                gpggaSentence.DifferentialGpsReferenceStationId = null;
            }

            return gpggaSentence;
        }

        /// <summary>
        ///     Converts a GPGGA sentence to its <see cref="GpggaSentence" /> equivalent. A return value indicates whether the
        ///     conversion succeeded.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="GpggaSentence" /> equivalent of the message
        ///     contained in <paramref name="sentence" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="sentence" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="sentence" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseGpgga(string sentence, out GpggaSentence result)
        {
            result = default(GpggaSentence);
            try
            {
                result = ParseGpgga(sentence);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a NMEA sentence to its managed equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="NmeaSentence" /> equivalent of the message
        ///     contained in <paramref name="sentence" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="sentence" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="sentence" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseBase(string sentence, out NmeaSentence result)
        {
            result = default(NmeaSentence);
            try
            {
                result = ParseBase(sentence);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a GPGLL sentence to its <see cref="GpgllSentence" /> equivalent.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <returns>The <see cref="GpgllSentence" /> equivalent to the sentence.</returns>
        public static GpgllSentence ParseGpgll(string sentence)
        {
            var gpgllSentence = new GpgllSentence(ParseBase(sentence));
            var fields = gpgllSentence.Fields;

            // Position (latitude/longitude)
            if (fields.Length > 3 && fields.All(Enumerable.Range(0, 4), s => !string.IsNullOrWhiteSpace(s)))
            {
                gpgllSentence.Position = Position.Parse(fields.ToArray(0, 4));
            }

            // UTC time of position
            if (fields.Length > 4 && !string.IsNullOrWhiteSpace(fields[4]))
            {
                var utcString = fields[4];
                var utcHours = int.Parse(utcString.Substring(0, 2));
                var utcMinutes = int.Parse(utcString.Substring(2, 2));
                var utcSeconds = int.Parse(utcString.Substring(4, 2));
                if (utcString.Contains(TimeSpanMillisecondsDelimiter, out int utcMillisecondsIndex))
                {
                    var utcMilliseconds = int.Parse(utcString.Substring(utcMillisecondsIndex + 1, utcString.Length - (utcMillisecondsIndex + 1))) * 1000;
                    gpgllSentence.UtcTime = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
                }
                else
                {
                    gpgllSentence.UtcTime = new TimeSpan(utcHours, utcMinutes, utcSeconds);
                }
            }

            // Position fix
            if (fields.Length > 5 && !string.IsNullOrWhiteSpace(fields[5]))
            {
                gpgllSentence.IsFix = Fix.ParseFix(fields[5]);
            }

            return gpgllSentence;
        }

        /// <summary>
        ///     Converts a GPGLL sentence to its <see cref="GpgllSentence" /> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="GpgllSentence" /> equivalent of the message
        ///     contained in <paramref name="sentence" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="sentence" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="sentence" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseGpgll(string sentence, out GpgllSentence result)
        {
            result = default(GpgllSentence);
            try
            {
                result = ParseGpgll(sentence);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a GPGSV sentence to its <see cref="GpgsaSentence" /> equivalent.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <returns>The <see cref="GpgsaSentence" /> equivalent to the sentence.</returns>
        public static GpgsaSentence ParseGpgsa(string sentence)
        {
            var gpgsaSentence = new GpgsaSentence(ParseBase(sentence));
            var fields = gpgsaSentence.Fields;
            if (fields.Length < 17)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Fix mode
            if (!string.IsNullOrWhiteSpace(fields[0]))
            {
                gpgsaSentence.FixMode = Fix.ParseMode(fields[0]);
            }

            // Fix plane
            if (!string.IsNullOrWhiteSpace(fields[1]))
            {
                gpgsaSentence.FixPlane = Fix.ParsePlane(fields[1]);
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
                gpgsaSentence.AddSatellite(pseudoRandomNoise);
            }

            // Position Dilution of Precision
            if (!string.IsNullOrWhiteSpace(fields[14]))
            {
                gpgsaSentence.PositionDilutionOfPrecision = DilutionOfPrecision.Parse(fields[14]);
            }

            // Horizontal Dilution of Precision
            if (!string.IsNullOrWhiteSpace(fields[15]))
            {
                gpgsaSentence.HorizontalDilutionOfPrecision = DilutionOfPrecision.Parse(fields[15]);
            }

            // Vertical Dilution of Precision
            if (!string.IsNullOrWhiteSpace(fields[16]))
            {
                gpgsaSentence.VerticalDilutionOfPrecision = DilutionOfPrecision.Parse(fields[16]);
            }

            return gpgsaSentence;
        }

        /// <summary>
        ///     Converts a GPGSA sentence to its <see cref="GpgsaSentence" /> equivalent. A return value indicates whether the
        ///     conversion succeeded.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="GpgsaSentence" /> equivalent of the message
        ///     contained in <paramref name="sentence" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="sentence" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="sentence" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseGpgsa(string sentence, out GpgsaSentence result)
        {
            result = default(GpgsaSentence);
            try
            {
                result = ParseGpgsa(sentence);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a GPGSV sentence to its <see cref="GpgsvSentence" /> equivalent.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <returns>The <see cref="GpgsvSentence" /> equivalent to the sentence.</returns>
        public static GpgsvSentence ParseGpgsv(string sentence)
        {
            var gpgsvSentence = new GpgsvSentence(ParseBase(sentence));
            var fields = gpgsvSentence.Fields;
            if (fields.Length <= 2)
            {
                throw new FormatException("Invalid NMEA data format");
            }

            // Total number of messages of this type in this cycle
            gpgsvSentence.MessagesCount = int.Parse(fields[0]);

            // Message number
            gpgsvSentence.MessageNumber = int.Parse(fields[1]);

            // Total number of visible satellites
            if (!string.IsNullOrWhiteSpace(fields[2]))
            {
                gpgsvSentence.VisibleSatellitesCount = int.Parse(fields[2]);
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
                    azimuth = Azimuth.Parse(fields[currentFieldIndex + 2]);
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
                gpgsvSentence.AddSatellite(new Satellite(pseudoRandomNoise, elevation, azimuth, signalToNoiseRatio));
            }

            return gpgsvSentence;
        }

        /// <summary>
        ///     Converts a GPGSV sentence to its <see cref="GpgsvSentence" /> equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="GpgsvSentence" /> equivalent of the message
        ///     contained in <paramref name="sentence" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="sentence" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="sentence" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseGpgsv(string sentence, out GpgsvSentence result)
        {
            result = default(GpgsvSentence);
            try
            {
                result = ParseGpgsv(sentence);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Converts a GPRMC sentence to its <see cref="GprmcSentence" /> equivalent.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <returns>The <see cref="GprmcSentence" /> equivalent to the sentence.</returns>
        public static GprmcSentence ParseGprmc(string sentence)
        {
            var gprmcSentence = new GprmcSentence(ParseBase(sentence));
            var fields = gprmcSentence.Fields;

            // UTC time of position
            var timeSpan = TimeSpan.Zero;
            if (fields.Length > 0 && !string.IsNullOrWhiteSpace(fields[0]))
            {
                var utcTimeString = fields[0];
                var utcHours = int.Parse(utcTimeString.Substring(0, 2));
                var utcMinutes = int.Parse(utcTimeString.Substring(2, 2));
                var utcSeconds = int.Parse(utcTimeString.Substring(4, 2));
                if (utcTimeString.Contains(TimeSpanMillisecondsDelimiter, out int utcMillisecondsIndex))
                {
                    var utcMilliseconds = int.Parse(utcTimeString.Substring(utcMillisecondsIndex + 1, utcTimeString.Length - (utcMillisecondsIndex + 1))) * 1000;
                    timeSpan = new TimeSpan(0, utcHours, utcMinutes, utcSeconds, utcMilliseconds);
                }
                else
                {
                    timeSpan = new TimeSpan(utcHours, utcMinutes, utcSeconds);
                }
            }

            // Fix mode
            if (fields.Length > 1 && !string.IsNullOrWhiteSpace(fields[1]))
            {
                gprmcSentence.NavigationState = Navigation.ParseNavigationState(fields[1]);
            }

            // Position (latitude/longitude)
            if (fields.Length > 2 && fields.All(Enumerable.Range(2, 4), s => !string.IsNullOrWhiteSpace(s)))
            {
                gprmcSentence.Position = Position.Parse(gprmcSentence.Fields.ToArray(2, 4));
            }

            // Speed
            if (fields.Length > 6 && !string.IsNullOrWhiteSpace(fields[6]))
            {
                gprmcSentence.Speed = Speed.Parse(SpeedUnit.Knots, gprmcSentence.Fields[6]);
            }

            // Bearing (Course)
            if (fields.Length > 7 && !string.IsNullOrWhiteSpace(fields[7]))
            {
                gprmcSentence.Bearing = Azimuth.Parse(fields[7]);
            }

            // Parse the UTC date
            var date = DateTime.MinValue;
            if (fields.Length > 8 && !string.IsNullOrWhiteSpace(fields[8]))
            {
                var utcDateString = fields[8];
                var utcDay = int.Parse(utcDateString.Substring(0, 2));
                var utcMonth = int.Parse(utcDateString.Substring(2, 2));
                var utcYear = int.Parse(utcDateString.Substring(4, 2));
                date = new DateTime(2000 + utcYear, utcMonth, utcDay);
            }

            // Merge fix date & time values
            gprmcSentence.FixUtcDateTime = new DateTime(date.Year, date.Month, date.Day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            // Verify that a magnetic variation exists; otherwise use an empty value
            if (fields.Length > 10 && !string.IsNullOrWhiteSpace(fields[9]) && !string.IsNullOrWhiteSpace(fields[10]))
            {
                gprmcSentence.MagneticVariation = new Longitude(Sexagesimal.Parse(fields[9]), Longitude.ParseHemisphere(fields[10]));
            }

            return gprmcSentence;
        }

        /// <summary>
        ///     Converts a GPRMC sentence to its <see cref="GprmcSentence" /> equivalent. A return value indicates whether the
        ///     conversion succeeded.
        /// </summary>
        /// <param name="sentence">A string containing a sentence to convert.</param>
        /// <param name="result">
        ///     When this method returns, contains the <see cref="GprmcSentence" /> equivalent of the message
        ///     contained in <paramref name="sentence" />, if the conversion succeeded or null if the conversion failed. The
        ///     conversion fails if the <paramref name="sentence" /> parameter is null or is not of the correct format. This
        ///     parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.
        /// </param>
        /// <returns><bold>true</bold> if <paramref name="sentence" /> was converted successfully; otherwise, <bold>false</bold>.</returns>
        public static bool TryParseGprmc(string sentence, out GprmcSentence result)
        {
            result = default(GprmcSentence);
            try
            {
                result = ParseGprmc(sentence);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}