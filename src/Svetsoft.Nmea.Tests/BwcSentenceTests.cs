using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class BwcSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed BWC sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseBwcSentence()
        {
            var sentence = new BwcSentence("$GPBWC,220516,5130.02,N,00046.34,W,213.8,T,218.0,M,0004.6,N,EGLM*21");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPBWC,220516,5130.02,N,00046.34,W,213.8,T,218.0,M,0004.6,N,EGLM*21", sentence.Sentence);
            Assert.AreEqual("GPBWC", sentence.MessageType);
            Assert.AreEqual("21", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "220516",
                "5130.02",
                "N",
                "00046.34",
                "W",
                "213.8",
                "T",
                "218.0",
                "M",
                "0004.6",
                "N",
                "EGLM"
            }, sentence.Fields);

            // BWC-specific
            Assert.AreEqual(new TimeSpan(22, 05, 16), sentence.UtcTime);
            Assert.AreEqual(5130.02, sentence.WaypointPosition.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.North, sentence.WaypointPosition.Latitude.Hemisphere);
            Assert.AreEqual(00046.34, sentence.WaypointPosition.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.West, sentence.WaypointPosition.Longitude.Hemisphere);
            Assert.AreEqual(213.8, sentence.TrueBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.True, sentence.TrueBearing.AbsoluteBearing);
            Assert.AreEqual(218.0, sentence.MagneticBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.MagneticBearing.AbsoluteBearing);
            Assert.AreEqual(0004.6, sentence.WaypointDistance.Value);
            Assert.AreEqual(DistanceUnit.NauticalMiles, sentence.WaypointDistance.Unit);
            Assert.AreEqual("EGLM", sentence.Waypoint.Name);
        }
    }
}