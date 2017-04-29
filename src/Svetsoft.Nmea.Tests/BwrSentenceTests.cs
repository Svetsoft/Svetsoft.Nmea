using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class BwrSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed BWR sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseBwrSentence()
        {
            var sentence = new BwrSentence("$GPBWR,220516,5130.02,N,00046.34,W,213.8,T,218.0,M,0004.6,N,EGLM*30");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPBWR,220516,5130.02,N,00046.34,W,213.8,T,218.0,M,0004.6,N,EGLM*30", sentence.Sentence);
            Assert.AreEqual("GPBWR", sentence.MessageType);
            Assert.AreEqual("30", sentence.Checksum);
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

            // BWR-specific
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