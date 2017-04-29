using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class BwwSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed BWW sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseBwwSentence()
        {
            var sentence = new BwwSentence("$GPBWW,011,T,011,M,DEST,ORIG*59");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPBWW,011,T,011,M,DEST,ORIG*59", sentence.Sentence);
            Assert.AreEqual("GPBWW", sentence.MessageType);
            Assert.AreEqual("59", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "011",
                "T",
                "011",
                "M",
                "DEST",
                "ORIG"
            }, sentence.Fields);

            // BWW-specific
            Assert.AreEqual(011, sentence.TrueBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.True, sentence.TrueBearing.AbsoluteBearing);
            Assert.AreEqual(011, sentence.MagneticBearing.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.MagneticBearing.AbsoluteBearing);
            Assert.AreEqual("DEST", sentence.DestinationWaypoint.Name);
            Assert.AreEqual("ORIG", sentence.OriginWaypoint.Name);
        }
    }
}