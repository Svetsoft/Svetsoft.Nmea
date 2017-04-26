using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class BecSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed BEC sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseBecSentence()
        {
            var sentence = new BecSentence("$GPBEC,170834,4124.8963,N,08151.6838,W,011,T,011,M,000.0,N,DEST*03");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPBEC,170834,4124.8963,N,08151.6838,W,011,T,011,M,000.0,N,DEST*03", sentence.Sentence);
            Assert.AreEqual("GPBEC", sentence.MessageType);
            Assert.AreEqual("03", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "170834",
                "4124.8963",
                "N",
                "08151.6838",
                "W",
                "011",
                "T",
                "011",
                "M",
                "000.0",
                "N",
                "DEST"
            }, sentence.Fields);

            // BEC-specific
            Assert.AreEqual(new TimeSpan(17, 08, 34), sentence.UtcTime);
            Assert.AreEqual(4124.8963, sentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.North, sentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(8151.6838, sentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.West, sentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(011, sentence.TrueBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.True, sentence.TrueBearing.AbsoluteBearing);
            Assert.AreEqual(011, sentence.MagneticBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.MagneticBearing.AbsoluteBearing);
            Assert.AreEqual(0.0, sentence.Speed.Value);
            Assert.AreEqual(SpeedUnit.Knots, sentence.Speed.Unit);
            Assert.AreEqual("DEST", sentence.DestinationWaypoint.Name);
        }
    }
}