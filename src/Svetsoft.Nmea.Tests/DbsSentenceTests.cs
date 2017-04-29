using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class DbsSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed DBS sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseDbsSentence()
        {
            var sentence = new DbsSentence("$GPDBS,20.0,f,20.0,M,20.0,F*33");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPDBS,20.0,f,20.0,M,20.0,F*33", sentence.Sentence);
            Assert.AreEqual("GPDBS", sentence.MessageType);
            Assert.AreEqual("33", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "20.0",
                "f",
                "20.0",
                "M",
                "20.0",
                "F"
            }, sentence.Fields);

            // DBS-specific
            Assert.AreEqual(20.0, sentence.FeetDepth.Value);
            Assert.AreEqual(DistanceUnit.Feet, sentence.FeetDepth.Unit);
            Assert.AreEqual(20.0, sentence.MetersDepth.Value);
            Assert.AreEqual(DistanceUnit.Meters, sentence.MetersDepth.Unit);
            Assert.AreEqual(20.0, sentence.FathomsDepth.Value);
            Assert.AreEqual(DistanceUnit.Fathoms, sentence.FathomsDepth.Unit);
        }
    }
}