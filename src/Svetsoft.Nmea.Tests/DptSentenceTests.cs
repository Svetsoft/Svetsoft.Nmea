using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class DptSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed DPT sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseDptSentence()
        {
            var sentence = new DptSentence("$GPDPT,20.0,M,-5.0*2C");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPDPT,20.0,M,-5.0*2C", sentence.Sentence);
            Assert.AreEqual("GPDPT", sentence.MessageType);
            Assert.AreEqual("2C", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "20.0",
                "M",
                "-5.0"
            }, sentence.Fields);

            // DPT-specific
            Assert.AreEqual(20.0, sentence.Depth.Value);
            Assert.AreEqual(DistanceUnit.Meters, sentence.Depth.Unit);
            Assert.AreEqual(-5.0, sentence.TransducerOffset);
        }
    }
}