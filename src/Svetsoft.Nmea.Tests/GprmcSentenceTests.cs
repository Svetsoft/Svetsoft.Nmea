using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class GprmcSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed GPRMC sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseGprmcSentence()
        {
            var gprmcSentence = new GprmcSentence("$GPRMC,081836,A,3751.65,S,14507.36,E,000.0,360.0,130998,011.3,E*62");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPRMC,081836,A,3751.65,S,14507.36,E,000.0,360.0,130998,011.3,E*62", gprmcSentence.Sentence);
            Assert.AreEqual("GPRMC", gprmcSentence.MessageType);
            Assert.AreEqual("62", gprmcSentence.Checksum);
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
            }, gprmcSentence.Fields);

            // GPRMC-specific
            Assert.AreEqual(NavigationState.Valid, gprmcSentence.NavigationState);
            Assert.AreEqual(3751.65, gprmcSentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.South, gprmcSentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(14507.36, gprmcSentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.East, gprmcSentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(0.0, gprmcSentence.Speed.Value);
            Assert.AreEqual(SpeedUnit.Knots, gprmcSentence.Speed.Unit);
            Assert.AreEqual(360.0, gprmcSentence.Bearing.Sexagesimal.Degrees);
            Assert.AreEqual(new DateTime(2098, 09, 13, 08, 18, 36), gprmcSentence.FixUtcDateTime);
            Assert.AreEqual(011.3, gprmcSentence.MagneticVariation.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.East, gprmcSentence.MagneticVariation.Hemisphere);
        }
    }
}