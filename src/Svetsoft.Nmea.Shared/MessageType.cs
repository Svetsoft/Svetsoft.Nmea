using System;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents a type of message in the NMEA specification.
    /// </summary>
    public sealed class MessageType
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="MessageType" /> class.
        /// </summary>
        /// <param name="sentence">The sentence that identifies the type of message.</param>
        /// <param name="type">The .NET type for the message.</param>
        public MessageType(string sentence, Type type)
        {
            Sentence = sentence;
            Type = type;
        }

        /// <summary>
        ///     Returns the sentence that identifies this type of message.
        /// </summary>
        public string Sentence { get; }

        /// <summary>
        ///     Returns the .NET type for this message.
        /// </summary>
        public Type Type { get; }
    }
}