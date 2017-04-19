using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class BodSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed BOD sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseBodSentence()
        {
            var sentence = new BodSentence("$GPBOD,011,T,011,M,DEST,ORIG*52");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPBOD,011,T,011,M,DEST,ORIG*52", sentence.Sentence);
            Assert.AreEqual("GPBOD", sentence.MessageType);
            Assert.AreEqual("52", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "011",
                "T",
                "011",
                "M",
                "DEST",
                "ORIG"
            }, sentence.Fields);

            // BOD-specific
            Assert.AreEqual(011, sentence.TrueBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.True, sentence.TrueBearing.AbsoluteBearing);
            Assert.AreEqual(011, sentence.MagneticBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.MagneticBearing.AbsoluteBearing);
            Assert.AreEqual("DEST", sentence.DestinationWaypointId);
            Assert.AreEqual("ORIG", sentence.OriginWaypointId);
        }
    }
}