using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Geography.Positioning.Tests
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
            var gpgsaSentence = NmeaSentence.ParseGpgsa("$GPGSA,A,2,25,31,05,,,,,,,,,,4.1,2.6,3.2*33");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGSA,A,2,25,31,05,,,,,,,,,,4.1,2.6,3.2*33", gpgsaSentence.Sentence);
            Assert.AreEqual("GPGSA", gpgsaSentence.MessageType);
            Assert.AreEqual("33", gpgsaSentence.Checksum);
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
            }, gpgsaSentence.Fields);

            // GPGSA-specific
            Assert.AreEqual(FixMode.Automatic, gpgsaSentence.FixMode);
            Assert.AreEqual(FixPlane.ThreeDimensional, gpgsaSentence.FixPlane);
            Assert.AreEqual("25", gpgsaSentence.SatellitePrns[0].Value);
            Assert.AreEqual(25, gpgsaSentence.SatellitePrns[0].Number);
            Assert.AreEqual("31", gpgsaSentence.SatellitePrns[1].Value);
            Assert.AreEqual(31, gpgsaSentence.SatellitePrns[1].Number);
            Assert.AreEqual("05", gpgsaSentence.SatellitePrns[2].Value);
            Assert.AreEqual(5, gpgsaSentence.SatellitePrns[2].Number);
            Assert.AreEqual(4.1f, gpgsaSentence.PositionDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, gpgsaSentence.PositionDilutionOfPrecision.Rating);
            Assert.AreEqual(2.6f, gpgsaSentence.HorizontalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, gpgsaSentence.HorizontalDilutionOfPrecision.Rating);
            Assert.AreEqual(3.2f, gpgsaSentence.VerticalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, gpgsaSentence.VerticalDilutionOfPrecision.Rating);
        }

        /// <summary>
        ///     Checks whether a parsed GPGSA sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void TryParseNmeaSentence()
        {
            var result = NmeaSentence.TryParseGpgsa("$GPGSA,A,2,25,31,05,,,,,,,,,,4.1,2.6,3.2*33", out GpgsaSentence gpgsaSentence);

            Assert.AreEqual(true, result);

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGSA,A,2,25,31,05,,,,,,,,,,4.1,2.6,3.2*33", gpgsaSentence.Sentence);
            Assert.AreEqual("GPGSA", gpgsaSentence.MessageType);
            Assert.AreEqual("33", gpgsaSentence.Checksum);
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
            }, gpgsaSentence.Fields);

            // GPGSA-specific
            Assert.AreEqual(FixMode.Automatic, gpgsaSentence.FixMode);
            Assert.AreEqual(FixPlane.ThreeDimensional, gpgsaSentence.FixPlane);
            Assert.AreEqual("25", gpgsaSentence.SatellitePrns[0].Value);
            Assert.AreEqual(25, gpgsaSentence.SatellitePrns[0].Number);
            Assert.AreEqual("31", gpgsaSentence.SatellitePrns[1].Value);
            Assert.AreEqual(31, gpgsaSentence.SatellitePrns[1].Number);
            Assert.AreEqual("05", gpgsaSentence.SatellitePrns[2].Value);
            Assert.AreEqual(5, gpgsaSentence.SatellitePrns[2].Number);
            Assert.AreEqual(4.1f, gpgsaSentence.PositionDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, gpgsaSentence.PositionDilutionOfPrecision.Rating);
            Assert.AreEqual(2.6f, gpgsaSentence.HorizontalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, gpgsaSentence.HorizontalDilutionOfPrecision.Rating);
            Assert.AreEqual(3.2f, gpgsaSentence.VerticalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Good, gpgsaSentence.VerticalDilutionOfPrecision.Rating);
        }
    }
}