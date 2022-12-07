using NUnit.Framework;
using System;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private string driverName;
        private UnitDriver driver;
        private RaceEntry raceEntry;        

        [SetUp]
        public void Setup()
        {
            driverName = "Pesho";
            raceEntry = new RaceEntry();            
            driver = new UnitDriver(driverName, new UnitCar("Model", 200, 2000));
        }

        [Test]
        public void CtorAndCounterTest()
        {
            Assert.IsTrue(raceEntry.Counter == 0);
        }

        [Test]
        public void AddDriverShouldThrowIfDriverIsNull()
        {
            UnitDriver nullDriver = null;

            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(nullDriver));
        }

        [Test]
        public void AddDriverShouldThrowIfExistDriverName()
        {
            raceEntry.AddDriver(driver);

            UnitDriver sameNameDriver = new UnitDriver(driverName, new UnitCar("new", 500, 4000));

            Assert.Throws<InvalidOperationException>(() => raceEntry.AddDriver(sameNameDriver));
        }

        [Test]
        public void AddDriver_Test()
        {
            string expectedOutput = $"Driver {driverName} added in race.";

            Assert.AreEqual(expectedOutput, raceEntry.AddDriver(driver));
            Assert.IsTrue(raceEntry.Counter == 1);
        }

        [Test]
        public void CalculateAverageHorsePowerThrowsIfParticipantsLessThanTwo() 
        {
            raceEntry.AddDriver(driver);

            Assert.Throws<InvalidOperationException>(() => raceEntry.CalculateAverageHorsePower());
        }

        [Test]
        public void CalculateAverageHorsePower_Test()
        {
            raceEntry.AddDriver(driver);

            var driverTwo = new UnitDriver("Gosho", new UnitCar("newModel", 300, 2500));
            raceEntry.AddDriver(driverTwo);

            double expectedAveragePower = (200 + 300) / 2;

            Assert.AreEqual(expectedAveragePower, raceEntry.CalculateAverageHorsePower());
        }
    }
}