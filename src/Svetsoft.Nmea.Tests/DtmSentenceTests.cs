using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class DtmSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed DTM sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseDtmSentence()
        {
            var sentence = new DtmSentence("$GPDTM,W84,,4124.8963,N,08151.6838,W,011,W84*5C");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPDTM,W84,,4124.8963,N,08151.6838,W,011,W84*5C", sentence.Sentence);
            Assert.AreEqual("GPDTM", sentence.MessageType);
            Assert.AreEqual("5C", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "W84",
                "",
                "4124.8963",
                "N",
                "08151.6838",
                "W",
                "011",
                "W84"
            }, sentence.Fields);

            // DTM-specific
            Assert.AreEqual("W84", sentence.DatumCode);
            Assert.IsNull(sentence.DatumSubCode);
            Assert.AreEqual(4124.8963, sentence.PositionOffset.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.North, sentence.PositionOffset.Latitude.Hemisphere);
            Assert.AreEqual(8151.6838, sentence.PositionOffset.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.West, sentence.PositionOffset.Longitude.Hemisphere);
            Assert.AreEqual(011, sentence.AltitudeOffset.Value);
            Assert.AreEqual(DistanceUnit.Meters, sentence.AltitudeOffset.Unit);
            Assert.AreEqual("W84", sentence.DatumName);
        }
    }
}