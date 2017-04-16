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
            var gpgllSentence = new GpgllSentence("$GPGLL,3751.65,S,14507.36,E,180133.35*7B");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGLL,3751.65,S,14507.36,E,180133.35*7B", gpgllSentence.Sentence);
            Assert.AreEqual("GPGLL", gpgllSentence.MessageType);
            Assert.AreEqual("7B", gpgllSentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "3751.65",
                "S",
                "14507.36",
                "E",
                "180133.35"
            }, gpgllSentence.Fields);

            // GPGLL-specific
            Assert.AreEqual(3751.65, gpgllSentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.South, gpgllSentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(14507.36, gpgllSentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.East, gpgllSentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(new TimeSpan(0, 18, 01, 33, 35000), gpgllSentence.UtcTime);
            Assert.AreEqual(false, gpgllSentence.IsFix);
        }
    }
}