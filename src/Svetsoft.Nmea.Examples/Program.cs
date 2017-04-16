using System;
using System.IO.Ports;

namespace Svetsoft.Nmea.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serialPorts = SerialPort.GetPortNames();
            if (serialPorts.Length <= 0)
            {
                Console.WriteLine("Please, connect a GPS device and try again.");
            }
            else
            {
                var nmeaInterpreter = new NmeaInterpreter(serialPorts[0]);
                nmeaInterpreter.ParsedNmeaSentence += OnParsedNmeaSentence;
                nmeaInterpreter.Start();
            }

            Console.ReadLine();
        }

        private static void OnParsedNmeaSentence(NmeaSentence sentence)
        {
            Console.WriteLine($"Parsed {sentence.MessageType} sentence.");
        }
    }
}