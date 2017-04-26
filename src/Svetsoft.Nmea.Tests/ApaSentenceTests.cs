using System;
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
        [ExpectedException(typeof(NotImplementedException), "ApaSentence is not implemented")]
        public void ParseApaSentence()
        {
            var sentence = new ApaSentence("$GPAPA,A,A,0.10,R,N,V,V,011,M,DEST*3F");

            // NmeaSentence (Inherited)
            Assert.AreEqual("$GPAPA,A,A,0.10,R,N,V,V,011,M,DEST*3F", sentence.Sentence);
            Assert.AreEqual("GPAPA", sentence.MessageType);
            Assert.AreEqual("3F", sentence.Checksum);
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
                "DEST"
            }, sentence.Fields);

            // APA-specific
            Assert.AreEqual(false, sentence.IsLoranCBlinkActive);
            Assert.AreEqual(false, sentence.IsLoranCCycleLockActive);
            Assert.AreEqual(0.10, sentence.CrossTrackErrorMagnitude);
            Assert.AreEqual(SteeringDirection.Right, sentence.SteeringDirection);
            Assert.AreEqual(DistanceUnit.NauticalMiles, sentence.CrossTrackUnits);
            Assert.AreEqual(false, sentence.IsArrivalCircleEntered);
            Assert.AreEqual(false, sentence.IsPerpendicularPassedAtWaypoint);
            Assert.AreEqual(011, sentence.BearingOriginToDestination.Value.Sexagesimal.Degrees);
            Assert.AreEqual(AbsoluteBearing.Magnetic, sentence.BearingOriginToDestination.AbsoluteBearing);
            Assert.AreEqual("DEST", sentence.DestinationWaypoint.Name);
        }
    }
}