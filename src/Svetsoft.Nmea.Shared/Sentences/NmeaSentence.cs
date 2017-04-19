using System;

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
        protected const int MaximumSatellitesPerSentenceCount = 6;
        protected const int FieldsPerSatelliteCount = 4;
        protected const int FieldsSkippedCount = 3;

        /// <summary>
        /// Creates a new instance of the <see cref="NmeaSentence"/> class.
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