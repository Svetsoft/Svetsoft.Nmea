using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Geography.Positioning.Tests
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
            var gpggaSentence = NmeaSentence.ParseGpgga("$GPGGA,170834,4124.8963,N,08151.6838,W,1,05,1.5,280.2,M,-34.0,M,,,*59");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGGA,170834,4124.8963,N,08151.6838,W,1,05,1.5,280.2,M,-34.0,M,,,*59", gpggaSentence.Sentence);
            Assert.AreEqual("GPGGA", gpggaSentence.MessageType);
            Assert.AreEqual("59", gpggaSentence.Checksum);
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
            }, gpggaSentence.Fields);

            // GPGGA-specific
            Assert.AreEqual(new TimeSpan(17, 08, 34), gpggaSentence.UtcTime);
            Assert.AreEqual(4124.8963, gpggaSentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.North, gpggaSentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(8151.6838, gpggaSentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.West, gpggaSentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(FixQuality.NoFix, gpggaSentence.FixQuality);
            Assert.AreEqual(5, gpggaSentence.VisibleSatellitesCount);
            Assert.AreEqual(1.5, gpggaSentence.HorizontalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Excellent, gpggaSentence.HorizontalDilutionOfPrecision.Rating);
            Assert.AreEqual(280.2, gpggaSentence.Altitude.Value);
            Assert.AreEqual(DistanceUnit.Meters, gpggaSentence.Altitude.Unit);
            Assert.AreEqual(-34.0, gpggaSentence.GeoidalSeparator.Value);
            Assert.AreEqual(DistanceUnit.Meters, gpggaSentence.GeoidalSeparator.Unit);
            Assert.IsNull(gpggaSentence.SecondsSinceLastDifferentialGpsSc104Update);
            Assert.IsNull(gpggaSentence.DifferentialGpsReferenceStationId);
        }

        /// <summary>
        ///     Checks whether a parsed GPGGA sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void TryParseNmeaSentence()
        {
            var result = NmeaSentence.TryParseGpgga("$GPGGA,170834,4124.8963,N,08151.6838,W,1,05,1.5,280.2,M,-34.0,M,,,*59", out GpggaSentence gpggaSentence);

            Assert.AreEqual(true, result);

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPGGA,170834,4124.8963,N,08151.6838,W,1,05,1.5,280.2,M,-34.0,M,,,*59", gpggaSentence.Sentence);
            Assert.AreEqual("GPGGA", gpggaSentence.MessageType);
            Assert.AreEqual("59", gpggaSentence.Checksum);
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
            }, gpggaSentence.Fields);

            // GPGGA-specific
            Assert.AreEqual(new TimeSpan(17, 08, 34), gpggaSentence.UtcTime);
            Assert.AreEqual(4124.8963, gpggaSentence.Position.Latitude.Sexagesimal.Degrees);
            Assert.AreEqual(LatitudeHemisphere.North, gpggaSentence.Position.Latitude.Hemisphere);
            Assert.AreEqual(8151.6838, gpggaSentence.Position.Longitude.Sexagesimal.Degrees);
            Assert.AreEqual(LongitudeHemisphere.West, gpggaSentence.Position.Longitude.Hemisphere);
            Assert.AreEqual(FixQuality.NoFix, gpggaSentence.FixQuality);
            Assert.AreEqual(5, gpggaSentence.VisibleSatellitesCount);
            Assert.AreEqual(1.5, gpggaSentence.HorizontalDilutionOfPrecision.Value);
            Assert.AreEqual(DilutionOfPrecisionRating.Excellent, gpggaSentence.HorizontalDilutionOfPrecision.Rating);
            Assert.AreEqual(280.2, gpggaSentence.Altitude.Value);
            Assert.AreEqual(DistanceUnit.Meters, gpggaSentence.Altitude.Unit);
            Assert.AreEqual(-34.0, gpggaSentence.GeoidalSeparator.Value);
            Assert.AreEqual(DistanceUnit.Meters, gpggaSentence.GeoidalSeparator.Unit);
            Assert.IsNull(gpggaSentence.SecondsSinceLastDifferentialGpsSc104Update);
            Assert.IsNull(gpggaSentence.DifferentialGpsReferenceStationId);
        }
    }
}