using System;

namespace Svetsoft.Geography.Positioning
{
    /// <summary>
    ///     Represents the exception that is thrown when a checksum mismatch occurs.
    /// </summary>
    public class ChecksumMismatchException : Exception
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ChecksumMismatchException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ChecksumMismatchException(string message) : base(message)
        {
        }
    }
}