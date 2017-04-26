using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class AlmSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed ALM sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseAlmSentence()
        {
            var sentence = new AlmSentence("$GPALM,1,1,15,1159,00,441d,4e,16be,fd5e,a10c9f,4a2da4,686e81,58cbe1,0a4,001*77");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPALM,1,1,15,1159,00,441d,4e,16be,fd5e,a10c9f,4a2da4,686e81,58cbe1,0a4,001*77", sentence.Sentence);
            Assert.AreEqual("GPALM", sentence.MessageType);
            Assert.AreEqual("77", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "1",
                "1",
                "15",
                "1159",
                "00",
                "441d",
                "4e",
                "16be",
                "fd5e",
                "a10c9f",
                "4a2da4",
                "686e81",
                "58cbe1",
                "0a4",
                "001"
            }, sentence.Fields);

            // ALM-specific
            Assert.AreEqual(1, sentence.MessagesCount);
            Assert.AreEqual(1, sentence.MessageNumber);
            Assert.AreEqual(15, sentence.SatellitePrn.Number);
            Assert.AreEqual("15", sentence.SatellitePrn.Raw);
            Assert.AreEqual(1159, sentence.WeekNumber);
            Assert.AreEqual(0x00, sentence.SatelliteHealth);
            Assert.AreEqual(0x441d, sentence.Eccentricity);
            Assert.AreEqual(0x4e, sentence.AlmanacReferenceTime);
            Assert.AreEqual(0x16be, sentence.InclinationAngle);
            Assert.AreEqual(0xfd5e, sentence.RightAscensionRate);
            Assert.AreEqual(0xa10c9f, sentence.SemiMajorAxisRoot);
            Assert.AreEqual(0x4a2da4, sentence.PerigeeArgument);
            Assert.AreEqual(0x686e81, sentence.AscensionNodeLongitude);
            Assert.AreEqual(0x58cbe1, sentence.MeanAnomaly);
            Assert.AreEqual(0x0a4, sentence.F0ClockParameter);
            Assert.AreEqual(0x001, sentence.F1ClockParameter);
        }
    }
}