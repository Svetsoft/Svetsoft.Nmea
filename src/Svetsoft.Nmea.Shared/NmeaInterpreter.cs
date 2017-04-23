using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace Svetsoft.Nmea
{
    /// <summary>
    ///     Represents an interpreter for NMEA messages.
    /// </summary>
    public class NmeaInterpreter
    {
        public delegate void ParsedNmeaSentenceEvent(NmeaSentence sentence);

        protected const string CarriageReturnDelimiter = "\r";
        protected const string NewLineDelimiter = "\n";
        protected const string TagPattern = @"^\${0},";

        private readonly List<MessageType> _messageTypes;
        private bool _isStarted;

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaInterpreter" /> class.
        /// </summary>
        /// <param name="portName">The name of the port streaming data.</param>
        public NmeaInterpreter(string portName)
            : this(portName, 4800)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaInterpreter" /> class.
        /// </summary>
        /// <param name="port">The <see cref="Port" /> streaming data.</param>
        public NmeaInterpreter(SerialPort port)
        {
            _messageTypes = new List<MessageType>();
            Port = port;

            Initialize();
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaInterpreter" /> class.
        /// </summary>
        /// <param name="portName">The name of the port streaming data.</param>
        /// <param name="baudRate">The baud rate.</param>
        public NmeaInterpreter(string portName, int baudRate)
            : this(portName, baudRate, Parity.None)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaInterpreter" /> class.
        /// </summary>
        /// <param name="portName">The name of the port streaming data.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="parity">One of the <see cref="Parity" /> values.</param>
        public NmeaInterpreter(string portName, int baudRate, Parity parity)
            : this(portName, baudRate, parity, 8)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaInterpreter" /> class.
        /// </summary>
        /// <param name="portName">The name of the port streaming data.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="parity">One of the <see cref="SerialPort.Parity" /> values.</param>
        /// <param name="dataBits">The data bits value.</param>
        public NmeaInterpreter(string portName, int baudRate, Parity parity, int dataBits)
            : this(portName, baudRate, parity, dataBits, StopBits.One)
        {
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="NmeaInterpreter" /> class.
        /// </summary>
        /// <param name="portName">The name of the port streaming data.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="parity">One of the <see cref="SerialPort.Parity" /> values.</param>
        /// <param name="dataBits">The data bits value.</param>
        /// <param name="stopBits">One of the <see cref="SerialPort.StopBits" /> values.</param>
        public NmeaInterpreter(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
            : this(new SerialPort(portName, baudRate, parity, dataBits, stopBits))
        {
        }

        /// <summary>
        ///     Returns the list of <see cref="MessageType" /> elements parsed by this interpreter.
        /// </summary>
        public ReadOnlyCollection<MessageType> MessageTypes
        {
            get { return new ReadOnlyCollection<MessageType>(_messageTypes); }
        }

        /// <summary>
        ///     Returns the port streaming data.
        /// </summary>
        public SerialPort Port { get; }

        /// <summary>
        ///     Occurs when a sentence in NMEA format has been successfully parsed.
        /// </summary>
        public event ParsedNmeaSentenceEvent ParsedNmeaSentence;

        /// <summary>
        ///     Initializes this interpreter.
        /// </summary>
        private void Initialize()
        {
            // Talker sentences
            AddMessageType<GpggaSentence>("GPGGA");
            AddMessageType<GpgllSentence>("GPGLL");
            AddMessageType<GpgsaSentence>("GPGSA");
            AddMessageType<GpgsvSentence>("GPGSV");
            AddMessageType<GprmcSentence>("GPRMC");
        }

        /// <summary>
        ///     Adds a <see cref="MessageType" /> to the list of elements parsed by this interpreter.
        /// </summary>
        /// <typeparam name="T">The type of message.</typeparam>
        /// <param name="sentence">The sentence that identifies the type of message.</param>
        public void AddMessageType<T>(string sentence)
        {
            _messageTypes.Add(new MessageType(sentence, typeof(T)));
        }

        /// <summary>
        ///     Starts parsing streamed data with NMEA format.
        /// </summary>
        public void Start()
        {
            if (_isStarted)

            {
                return;
            }

            Port.DataReceived += OnDataReceived;
            Port.Open();

            _isStarted = true;
        }

        /// <summary>
        ///     Handles the data received event of the <see cref="SerialPort" /> object.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event data.</param>
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Parse(NormalizeNmea(Port.ReadLine()));
        }

        /// <summary>
        ///     Normalizes a string in NMEA format that is parsed by this interpreter.
        /// </summary>
        /// <param name="value">The string to normalize.</param>
        /// <returns>The normalized string.</returns>
        private string NormalizeNmea(string value)
        {
            value = value.Replace(CarriageReturnDelimiter, string.Empty);
            value = value.Replace(NewLineDelimiter, string.Empty);

            return value;
        }

        /// <summary>
        ///     Parses a string in NMEA format into its .NET equivalent.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        private void Parse(string value)
        {
            foreach (var messageType in _messageTypes)
            {
                if (!Regex.IsMatch(value, string.Format(TagPattern, messageType.Sentence)))
                {
                    continue;
                }

                var instance = Activator.CreateInstance(messageType.Type, value);
                if (instance == null)
                {
                    throw new TypeInitializationException(messageType.Type.FullName, null);
                }

                ParsedNmeaSentence?.Invoke((NmeaSentence) instance);
            }
        }
    }
}