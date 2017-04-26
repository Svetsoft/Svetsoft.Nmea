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
            var sentence = new AamSentence("$GPAAM,A,A,0.10,N,WPTNME*32");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPAAM,A,A,0.10,N,WPTNME*32", sentence.Sentence);
            Assert.AreEqual("GPAAM", sentence.MessageType);
            Assert.AreEqual("32", sentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "A",
                "A",
                "0.10",
                "N",
                "WPTNME"
            }, sentence.Fields);

            // AAM-specific
            Assert.AreEqual(Status.ActiveValue, sentence.IsArrivalCircleEntered);
            Assert.AreEqual(Status.ActiveValue, sentence.IsPerpendicularPassedAtWaypoint);
            Assert.AreEqual(0.10, sentence.ArrivalCircleRadius);
            Assert.AreEqual(DistanceUnit.NauticalMiles, sentence.RadiusUnit);
            Assert.AreEqual("WPTNME", sentence.Waypoint.Name);
        }
    }
}