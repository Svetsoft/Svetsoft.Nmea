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
            var gpgllSentence = new AlmSentence("$GPALM,1,1,15,1159,00,441d,4e,16be,fd5e,a10c9f,4a2da4,686e81,58cbe1,0a4,001*77");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPALM,1,1,15,1159,00,441d,4e,16be,fd5e,a10c9f,4a2da4,686e81,58cbe1,0a4,001*77", gpgllSentence.Sentence);
            Assert.AreEqual("GPALM", gpgllSentence.MessageType);
            Assert.AreEqual("77", gpgllSentence.Checksum);
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
            }, gpgllSentence.Fields);

            // ALM-specific
            Assert.AreEqual(1, gpgllSentence.MessagesCount);
            Assert.AreEqual(1, gpgllSentence.MessageNumber);
            Assert.AreEqual(15, gpgllSentence.SatellitePrn.Number);
            Assert.AreEqual("15", gpgllSentence.SatellitePrn.Value);
            Assert.AreEqual(1159, gpgllSentence.WeekNumber);
            Assert.AreEqual(0x00, gpgllSentence.SatelliteHealth);
            Assert.AreEqual(0x441d, gpgllSentence.Eccentricity);
            Assert.AreEqual(0x4e, gpgllSentence.AlmanacReferenceTime);
            Assert.AreEqual(0x16be, gpgllSentence.InclinationAngle);
            Assert.AreEqual(0xfd5e, gpgllSentence.RightAscensionRate);
            Assert.AreEqual(0xa10c9f, gpgllSentence.SemiMajorAxisRoot);
            Assert.AreEqual(0x4a2da4, gpgllSentence.PerigeeArgument);
            Assert.AreEqual(0x686e81, gpgllSentence.AscensionNodeLongitude);
            Assert.AreEqual(0x58cbe1, gpgllSentence.MeanAnomaly);
            Assert.AreEqual(0x0a4, gpgllSentence.F0ClockParameter);
            Assert.AreEqual(0x001, gpgllSentence.F1ClockParameter);
        }
    }
}