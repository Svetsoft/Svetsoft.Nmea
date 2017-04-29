using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class GsvSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed GSV sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseGsvSentence()
        {
            var sentence = new GsvSentence("$GPGSV,3,1,11,20,89,172,,12,64,158,,24,46,335,,15,38,018,*76");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGSV,3,1,11,20,89,172,,12,64,158,,24,46,335,,15,38,018,*76", sentence.Sentence);
            Assert.AreEqual("GPGSV", sentence.MessageType);
            Assert.AreEqual("76", sentence.Checksum);
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
            }, sentence.Fields);

            // GSV-specific
            Assert.AreEqual(3, sentence.MessagesCount);
            Assert.AreEqual(1, sentence.MessageNumber);
            Assert.AreEqual(11, sentence.SatellitesInViewCount);
            Assert.AreEqual(4, sentence.SatellitesInView.Count);

            // 1st satellite
            Assert.AreEqual("20", sentence.SatellitesInView[0].PseudoRandomNoise.Raw);
            Assert.AreEqual(20, sentence.SatellitesInView[0].PseudoRandomNoise.Number);
            Assert.AreEqual(89, sentence.SatellitesInView[0].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(172, sentence.SatellitesInView[0].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, sentence.SatellitesInView[0].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, sentence.SatellitesInView[0].SignalToNoiseRatio.Value);

            // 2nd satellite
            Assert.AreEqual("12", sentence.SatellitesInView[1].PseudoRandomNoise.Raw);
            Assert.AreEqual(12, sentence.SatellitesInView[1].PseudoRandomNoise.Number);
            Assert.AreEqual(64, sentence.SatellitesInView[1].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(158, sentence.SatellitesInView[1].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, sentence.SatellitesInView[1].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, sentence.SatellitesInView[1].SignalToNoiseRatio.Value);

            // 3rd satellite
            Assert.AreEqual("24", sentence.SatellitesInView[2].PseudoRandomNoise.Raw);
            Assert.AreEqual(24, sentence.SatellitesInView[2].PseudoRandomNoise.Number);
            Assert.AreEqual(46, sentence.SatellitesInView[2].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(335, sentence.SatellitesInView[2].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, sentence.SatellitesInView[2].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, sentence.SatellitesInView[2].SignalToNoiseRatio.Value);

            // 4th satellite
            Assert.AreEqual("15", sentence.SatellitesInView[3].PseudoRandomNoise.Raw);
            Assert.AreEqual(15, sentence.SatellitesInView[3].PseudoRandomNoise.Number);
            Assert.AreEqual(38, sentence.SatellitesInView[3].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(18, sentence.SatellitesInView[3].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, sentence.SatellitesInView[3].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, sentence.SatellitesInView[3].SignalToNoiseRatio.Value);
        }
    }
}