using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class RmcSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed RMC sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseGprmcSentence()
        {
            var sentence = new RmcSentence("$GPRMC,081836,A,3751.65,S,14507.36,E,000.0,360.0,130998,011.3,E*62");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPRMC,081836,A,3751.65,S,14507.36,E,000.0,360.0,130998,011.3,E*62", sentence.Sentence);
            Assert.AreEqual("GPRMC", sentence.MessageType);
            Assert.AreEqual("62", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "081836",
                "A",
                "3751.65",
                "S",
                "14507.36",
                "E",
                "000.0",
                "360.0",
                "130998",
                "011.3",
                "E"
            }, sentence.Fields);

            // RMC-specific
            Assert.AreEqual(Status.ActiveValue, sentence.NavigationState);
            Assert.AreEqual(3751.65, sentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.South, sentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(14507.36, sentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.East, sentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(0.0, sentence.Speed.Value);
            Assert.AreEqual(SpeedUnit.Knots, sentence.Speed.Unit);
            Assert.AreEqual(360.0, sentence.Bearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(new DateTime(2098, 09, 13, 08, 18, 36), sentence.FixUtcDateTime);
            Assert.AreEqual(011.3, sentence.MagneticVariation.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.East, sentence.MagneticVariation.Hemisphere);
        }
    }
}