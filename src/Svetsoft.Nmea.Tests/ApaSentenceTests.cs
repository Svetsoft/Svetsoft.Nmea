using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class ApaSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed APA sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseApaSentence()
        {
            var gpgllSentence = new ApaSentence("$GPAPA,A,A,0.10,R,N,V,V,011,M,DEST,011*23");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPAPA,A,A,0.10,R,N,V,V,011,M,DEST,011*23", gpgllSentence.Sentence);
            Assert.AreEqual("GPAPA", gpgllSentence.MessageType);
            Assert.AreEqual("23", gpgllSentence.Checksum);
            CollectionAssert.AreEqual(new[]
            {
                "A",
                "A",
                "0.10",
                "R",
                "N",
                "V",
                "V",
                "011",
                "M",
                "DEST",
                "011"
            }, gpgllSentence.Fields);

            // APA-specific
            Assert.AreEqual(false, gpgllSentence.IsLoranCBlinkActive);
            Assert.AreEqual(false, gpgllSentence.IsLoranCCycleLockActive);
            Assert.AreEqual(0.10, gpgllSentence.CrossTrackErrorMagnitude);
            Assert.AreEqual(SteeringDirection.Right, gpgllSentence.SteeringDirection);
            Assert.AreEqual(DistanceUnit.NauticalMiles, gpgllSentence.CrossTrackUnits);
            Assert.AreEqual(false, gpgllSentence.IsArrivalCircleEntered);
            Assert.AreEqual(false, gpgllSentence.IsPerpendicularPassedAtWaypoint);
            Assert.AreEqual(011, gpgllSentence.BearingOriginToDestination.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, gpgllSentence.AbsoluteBearing);
            Assert.AreEqual("DEST", gpgllSentence.DestinationWaypointId);
        }
    }
}