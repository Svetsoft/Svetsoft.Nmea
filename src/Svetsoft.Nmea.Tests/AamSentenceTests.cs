using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class AamSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed AAM sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseAamSentence()
        {
            var gpgllSentence = new AamSentence("$GPAAM,A,A,0.10,N,WPTNME*32");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPAAM,A,A,0.10,N,WPTNME*32", gpgllSentence.Sentence);
            Assert.AreEqual("GPAAM", gpgllSentence.MessageType);
            Assert.AreEqual("32", gpgllSentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "A",
                "A",
                "0.10",
                "N",
                "WPTNME"
            }, gpgllSentence.Fields);

            // AAM-specific
            Assert.AreEqual(true, gpgllSentence.IsArrivalCircleEntered);
            Assert.AreEqual(true, gpgllSentence.IsPerpendicularPassedAtWaypoint);
            Assert.AreEqual(0.10, gpgllSentence.ArrivalCircleRadius);
            Assert.AreEqual(DistanceUnit.NauticalMiles, gpgllSentence.RadiusUnit);
            Assert.AreEqual("WPTNME", gpgllSentence.WaypointId);
        }
    }
}