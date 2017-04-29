using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class DbkSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed DBK sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseDbkSentence()
        {
            var sentence = new DbkSentence("$GPDBK,20.0,f,20.0,M,20.0,F*2B");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPDBK,20.0,f,20.0,M,20.0,F*2B", sentence.Sentence);
            Assert.AreEqual("GPDBK", sentence.MessageType);
            Assert.AreEqual("2B", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "20.0",
                "f",
                "20.0",
                "M",
                "20.0",
                "F"
            }, sentence.Fields);

            // DBK-specific
            Assert.AreEqual(20.0, sentence.FeetDepth.Value);
            Assert.AreEqual(DistanceUnit.Feet, sentence.FeetDepth.Unit);
            Assert.AreEqual(20.0, sentence.MetersDepth.Value);
            Assert.AreEqual(DistanceUnit.Meters, sentence.MetersDepth.Unit);
            Assert.AreEqual(20.0, sentence.FathomsDepth.Value);
            Assert.AreEqual(DistanceUnit.Fathoms, sentence.FathomsDepth.Unit);
        }
    }
}