using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class GpgllSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed GPGLL sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseGpgllSentence()
        {
            var sentence = new GpgllSentence("$GPGLL,3751.65,S,14507.36,E,180133.35*7B");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGLL,3751.65,S,14507.36,E,180133.35*7B", sentence.Sentence);
            Assert.AreEqual("GPGLL", sentence.MessageType);
            Assert.AreEqual("7B", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "3751.65",
                "S",
                "14507.36",
                "E",
                "180133.35"
            }, sentence.Fields);

            // GPGLL-specific
            Assert.AreEqual(3751.65, sentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.South, sentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(14507.36, sentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.East, sentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(new TimeSpan(0, 18, 01, 33, 35000), sentence.UtcTime);
            Assert.AreEqual(false, sentence.IsFix);
        }
    }
}