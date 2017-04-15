using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Geography.Positioning.Tests
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
            var gpgsvSentence = NmeaSentence.ParseGpgsv("$GPGSV,3,1,11,20,89,172,,12,64,158,,24,46,335,,15,38,018,*76");

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
            Assert.AreEqual(11, gpgsvSentence.VisibleSatellitesCount);
            Assert.AreEqual(4, gpgsvSentence.Satellites.Count);

            // 1st satellite
            Assert.AreEqual("20", gpgsvSentence.Satellites[0].PseudoRandomNoise.Value);
            Assert.AreEqual(20, gpgsvSentence.Satellites[0].PseudoRandomNoise.Number);
            Assert.AreEqual(89, gpgsvSentence.Satellites[0].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(172, gpgsvSentence.Satellites[0].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[0].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[0].SignalToNoiseRatio.Value);

            // 2nd satellite
            Assert.AreEqual("12", gpgsvSentence.Satellites[1].PseudoRandomNoise.Value);
            Assert.AreEqual(12, gpgsvSentence.Satellites[1].PseudoRandomNoise.Number);
            Assert.AreEqual(64, gpgsvSentence.Satellites[1].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(158, gpgsvSentence.Satellites[1].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[1].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[1].SignalToNoiseRatio.Value);

            // 3rd satellite
            Assert.AreEqual("24", gpgsvSentence.Satellites[2].PseudoRandomNoise.Value);
            Assert.AreEqual(24, gpgsvSentence.Satellites[2].PseudoRandomNoise.Number);
            Assert.AreEqual(46, gpgsvSentence.Satellites[2].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(335, gpgsvSentence.Satellites[2].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[2].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[2].SignalToNoiseRatio.Value);

            // 4th satellite
            Assert.AreEqual("15", gpgsvSentence.Satellites[3].PseudoRandomNoise.Value);
            Assert.AreEqual(15, gpgsvSentence.Satellites[3].PseudoRandomNoise.Number);
            Assert.AreEqual(38, gpgsvSentence.Satellites[3].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(18, gpgsvSentence.Satellites[3].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[3].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[3].SignalToNoiseRatio.Value);
        }

        /// <summary>
        ///     Checks whether a parsed GPGSV sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void TryParseNmeaSentence()
        {
            var result = NmeaSentence.TryParseGpgsv("$GPGSV,3,1,11,20,89,172,,12,64,158,,24,46,335,,15,38,018,*76", out GpgsvSentence gpgsvSentence);

            // NmeaSentence (Inherited)
            Assert.AreEqual(true, result);
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
            Assert.AreEqual(11, gpgsvSentence.VisibleSatellitesCount);
            Assert.AreEqual(4, gpgsvSentence.Satellites.Count);

            // 1st satellite
            Assert.AreEqual("20", gpgsvSentence.Satellites[0].PseudoRandomNoise.Value);
            Assert.AreEqual(20, gpgsvSentence.Satellites[0].PseudoRandomNoise.Number);
            Assert.AreEqual(89, gpgsvSentence.Satellites[0].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(172, gpgsvSentence.Satellites[0].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[0].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[0].SignalToNoiseRatio.Value);

            // 2nd satellite
            Assert.AreEqual("12", gpgsvSentence.Satellites[1].PseudoRandomNoise.Value);
            Assert.AreEqual(12, gpgsvSentence.Satellites[1].PseudoRandomNoise.Number);
            Assert.AreEqual(64, gpgsvSentence.Satellites[1].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(158, gpgsvSentence.Satellites[1].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[1].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[1].SignalToNoiseRatio.Value);

            // 3rd satellite
            Assert.AreEqual("24", gpgsvSentence.Satellites[2].PseudoRandomNoise.Value);
            Assert.AreEqual(24, gpgsvSentence.Satellites[2].PseudoRandomNoise.Number);
            Assert.AreEqual(46, gpgsvSentence.Satellites[2].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(335, gpgsvSentence.Satellites[2].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[2].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[2].SignalToNoiseRatio.Value);

            // 4th satellite
            Assert.AreEqual("15", gpgsvSentence.Satellites[3].PseudoRandomNoise.Value);
            Assert.AreEqual(15, gpgsvSentence.Satellites[3].PseudoRandomNoise.Number);
            Assert.AreEqual(38, gpgsvSentence.Satellites[3].Elevation.Sexagesimal.Degrees);
            Assert.AreEqual(18, gpgsvSentence.Satellites[3].Azimuth.Sexagesimal.Degrees);
            Assert.AreEqual(SignalToNoiseRatioRating.Poor, gpgsvSentence.Satellites[3].SignalToNoiseRatio.Rating);
            Assert.AreEqual(0, gpgsvSentence.Satellites[3].SignalToNoiseRatio.Value);
        }
    }
}