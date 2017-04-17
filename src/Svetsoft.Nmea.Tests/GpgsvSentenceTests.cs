using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class GpgsvSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed GPGSV sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseGpgsvSentence()
        {
            var gpgsvSentence = new GpgsvSentence("$GPGSV,3,1,11,20,89,172,,12,64,158,,24,46,335,,15,38,018,*76");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGSV,3,1,11,20,89,172,,12,64,158,,24,46,335,,15,38,018,*76", gpgsvSentence.Sentence);
            Assert.AreEqual("GPGSV", gpgsvSentence.MessageType);
            Assert.AreEqual("76", gpgsvSentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "3",
                "1",
                "11",
                "20",
                "89",
                "172",
                "",
                "12",
                "64",
                "158",
                "",
                "24",
                "46",
                "335",
                "",
                "15",
                "38",
                "018",
                ""
            }, gpgsvSentence.Fields);

            // GPGSV-specific
            Assert.AreEqual(3, gpgsvSentence.MessagesCount);
            Assert.AreEqual(1, gpgsvSentence.MessageNumber);
            Assert.AreEqual(11, gpgsvSentence.SatellitesInViewCount);
            Assert.AreEqual(4, gpgsvSentence.SatellitesInView.Count);

            // 1st satellite
            Assert.AreEqual("20", gpgsvSentence.SatellitesInView[0].PseudoRandomNoise.Value);
            Assert.AreEqual(20, gpgsvSentence.SatellitesInView[0].PseudoRandomNoise.Number);
            Assert.AreEqual(89, gpgsvSentence.SatellitesInView[0].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(172, gpgsvSentence.SatellitesInView[0].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.SatellitesInView[0].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.SatellitesInView[0].SignalToNoiseRatio.Value);

            // 2nd satellite
            Assert.AreEqual("12", gpgsvSentence.SatellitesInView[1].PseudoRandomNoise.Value);
            Assert.AreEqual(12, gpgsvSentence.SatellitesInView[1].PseudoRandomNoise.Number);
            Assert.AreEqual(64, gpgsvSentence.SatellitesInView[1].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(158, gpgsvSentence.SatellitesInView[1].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.SatellitesInView[1].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.SatellitesInView[1].SignalToNoiseRatio.Value);

            // 3rd satellite
            Assert.AreEqual("24", gpgsvSentence.SatellitesInView[2].PseudoRandomNoise.Value);
            Assert.AreEqual(24, gpgsvSentence.SatellitesInView[2].PseudoRandomNoise.Number);
            Assert.AreEqual(46, gpgsvSentence.SatellitesInView[2].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(335, gpgsvSentence.SatellitesInView[2].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.SatellitesInView[2].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.SatellitesInView[2].SignalToNoiseRatio.Value);

            // 4th satellite
            Assert.AreEqual("15", gpgsvSentence.SatellitesInView[3].PseudoRandomNoise.Value);
            Assert.AreEqual(15, gpgsvSentence.SatellitesInView[3].PseudoRandomNoise.Number);
            Assert.AreEqual(38, gpgsvSentence.SatellitesInView[3].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(18, gpgsvSentence.SatellitesInView[3].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.SatellitesInView[3].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.SatellitesInView[3].SignalToNoiseRatio.Value);
        }
    }
}