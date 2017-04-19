using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Svetsoft.Nmea.Tests
{
    [TestClass]
    public class ApbSentenceTests
    {
        /// <summary>
        ///     Checks whether a parsed APB sentence from the NMEA specification equals the string it was built from.
        /// </summary>
        [TestMethod]
        public void ParseApbSentence()
        {
            var sentence = new ApbSentence("$GPAPB,A,A,0.10,R,N,V,V,011,M,DEST,011,M,011,M*3C");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPAPB,A,A,0.10,R,N,V,V,011,M,DEST,011,M,011,M*3C", sentence.Sentence);
            Assert.AreEqual("GPAPB", sentence.MessageType);
            Assert.AreEqual("3C", sentence.Checksum);
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
                "011",
                "M",
                "011",
                "M"
            }, sentence.Fields);

            // APB-specific
            Assert.AreEqual(false, sentence.IsLoranCBlinkActive);
            Assert.AreEqual(false, sentence.IsLoranCCycleLockActive);
            Assert.AreEqual(0.10, sentence.CrossTrackErrorMagnitude);
            Assert.AreEqual(SteeringDirection.Right, sentence.SteeringDirection);
            Assert.AreEqual(DistanceUnit.NauticalMiles, sentence.CrossTrackUnits);
            Assert.AreEqual(false, sentence.IsArrivalCircleEntered);
            Assert.AreEqual(false, sentence.IsPerpendicularPassedAtWaypoint);
            Assert.AreEqual(011, sentence.BearingOriginToDestination.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.BearingOriginToDestination.AbsoluteBearing);
            Assert.AreEqual("DEST", sentence.DestinationWaypointId);
            Assert.AreEqual(011, sentence.BearingCurrentPositionToDestination.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.BearingCurrentPositionToDestination.AbsoluteBearing);
            Assert.AreEqual(011, sentence.HeadingSteerToDestination.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.HeadingSteerToDestination.AbsoluteBearing);
        }
    }
}