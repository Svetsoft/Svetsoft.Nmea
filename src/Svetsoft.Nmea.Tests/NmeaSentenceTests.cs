using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Geography.Positioning.Tests
{
    [TestClass]
    public class NmeaSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed NMEA sentence equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseNmeaSentence()
        {
            var nmeaSentence = NmeaSentence.ParseBase("$GPGGA,004441.043,3454.928,N,07702.498,W,0,00,,,M,,M,,*51");

            Assert.AreEqual("$GPGGA,004441.043,3454.928,N,07702.498,W,0,00,,,M,,M,,*51", nmeaSentence.Sentence);
            Assert.AreEqual("GPGGA", nmeaSentence.MessageType);
            Assert.AreEqual("51", nmeaSentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "004441.043",
                "3454.928",
                "N",
                "07702.498",
                "W",
                "0",
                "00",
                "",
                "",
                "M",
                "",
                "M",
                "",
                ""
            }, nmeaSentence.Fields);
        }

        /// <summary>
        ///     Checks whether a parsed NMEA sentence equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void TryParseNmeaSentence()
        {
            var result = NmeaSentence.TryParseBase("$GPGGA,004441.043,3454.928,N,07702.498,W,0,00,,,M,,M,,*51", out NmeaSentence nmeaSentence);

            Assert.AreEqual(true, result);
            Assert.AreEqual("$GPGGA,004441.043,3454.928,N,07702.498,W,0,00,,,M,,M,,*51", nmeaSentence.Sentence);
            Assert.AreEqual("GPGGA", nmeaSentence.MessageType);
            Assert.AreEqual("51", nmeaSentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "004441.043",
                "3454.928",
                "N",
                "07702.498",
                "W",
                "0",
                "00",
                "",
                "",
                "M",
                "",
                "M",
                "",
                ""
            }, nmeaSentence.Fields);
        }
    }
}