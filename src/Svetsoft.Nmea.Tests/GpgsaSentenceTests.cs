using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class GpgsaSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed GPGSA sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseGpgsaSentence()
        {
            var sentence = new GpgsaSentence("$GPGSA,A,2,25,31,05,,,,,,,,,,4.1,2.6,3.2*33");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGSA,A,2,25,31,05,,,,,,,,,,4.1,2.6,3.2*33", sentence.Sentence);
            Assert.AreEqual("GPGSA", sentence.MessageType);
            Assert.AreEqual("33", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "A",
                "2",
                "25",
                "31",
                "05",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "4.1",
                "2.6",
                "3.2"
            }, sentence.Fields);

            // GPGSA-specific
            Assert.AreEqual(FixMode.Automatic, sentence.FixMode);
            Assert.AreEqual(FixPlane.ThreeDimensional, sentence.FixPlane);
            Assert.AreEqual("25", sentence.SatellitePrns[0].Raw);
            Assert.AreEqual(25, sentence.SatellitePrns[0].Number);
            Assert.AreEqual("31", sentence.SatellitePrns[1].Raw);
            Assert.AreEqual(31, sentence.SatellitePrns[1].Number);
            Assert.AreEqual("05", sentence.SatellitePrns[2].Raw);
            Assert.AreEqual(5, sentence.SatellitePrns[2].Number);
            Assert.AreEqual(4.1f, sentence.PositionDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, sentence.PositionDilutionOfPrecision.Rating);
            Assert.AreEqual(2.6f, sentence.HorizontalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, sentence.HorizontalDilutionOfPrecision.Rating);
            Assert.AreEqual(3.2f, sentence.VerticalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, sentence.VerticalDilutionOfPrecision.Rating);
        }
    }
}