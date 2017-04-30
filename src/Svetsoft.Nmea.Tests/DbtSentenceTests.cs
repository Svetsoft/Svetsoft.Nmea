using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class DbtSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed DBT sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseDbtSentence()
        {
            var sentence = new DbtSentence("$GPDBT,20.0,f,20.0,M,20.0,F*34");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPDBT,20.0,f,20.0,M,20.0,F*34", sentence.Sentence);
            Assert.AreEqual("GPDBT", sentence.MessageType);
            Assert.AreEqual("34", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "20.0",
                "f",
                "20.0",
                "M",
                "20.0",
                "F"
            }, sentence.Fields);

            // DBT-specific
            Assert.AreEqual(20.0, sentence.FeetDepth.Value);
            Assert.AreEqual(DistanceUnit.Feet, sentence.FeetDepth.Unit);
            Assert.AreEqual(20.0, sentence.MetersDepth.Value);
            Assert.AreEqual(DistanceUnit.Meters, sentence.MetersDepth.Unit);
            Assert.AreEqual(20.0, sentence.FathomsDepth.Value);
            Assert.AreEqual(DistanceUnit.Fathoms, sentence.FathomsDepth.Unit);
        }
    }
}