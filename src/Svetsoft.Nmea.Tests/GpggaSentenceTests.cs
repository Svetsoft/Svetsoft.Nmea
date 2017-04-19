using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class GpggaSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed GPGGA sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseGpggaSentence()
        {
            var sentence = new GpggaSentence("$GPGGA,170834,4124.8963,N,08151.6838,W,1,05,1.5,280.2,M,-34.0,M,,,*59");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGGA,170834,4124.8963,N,08151.6838,W,1,05,1.5,280.2,M,-34.0,M,,,*59", sentence.Sentence);
            Assert.AreEqual("GPGGA", sentence.MessageType);
            Assert.AreEqual("59", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "170834",
                "4124.8963",
                "N",
                "08151.6838",
                "W",
                "1",
                "05",
                "1.5",
                "280.2",
                "M",
                "-34.0",
                "M",
                "",
                "",
                ""
            }, sentence.Fields);

            // GPGGA-specific
            Assert.AreEqual(new TimeSpan(17, 08, 34), sentence.UtcTime);
            Assert.AreEqual(4124.8963, sentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.North, sentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(8151.6838, sentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.West, sentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(FixQuality.NoFix, sentence.FixQuality);
            Assert.AreEqual(5, sentence.SatellitesInViewCount);
            Assert.AreEqual(1.5, sentence.HorizontalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Excellent, sentence.HorizontalDilutionOfPrecision.Rating);
            Assert.AreEqual(280.2, sentence.Altitude.Value);
            Assert.AreEqual(DistanceUnit.Meters, sentence.Altitude.Unit);
            Assert.AreEqual(-34.0, sentence.GeoidalSeparator.Value);
            Assert.AreEqual(DistanceUnit.Meters, sentence.GeoidalSeparator.Unit);
            Assert.IsNull(sentence.SecondsSinceLastDifferentialGpsSc104Update);
            Assert.IsNull(sentence.DifferentialGpsReferenceStationId);
        }
    }
}