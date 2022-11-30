namespace Gyms.Tests
{
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class GymsTests
    {
        private Gym gym;

        [SetUp]
        public void SetUp()
        {
            gym = new Gym("Test", 2);
        }

        [Test]
        public void CtorTest()
        {
            string name = "Test";
            int size = 10;
            Gym gym = new Gym(name, size);

            Assert.AreEqual(name, gym.Name);
            Assert.AreEqual(size, gym.Capacity);
            Assert.AreEqual(0, gym.Count);           
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameThrowsIfNullOrEmpty(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Gym(name, 5));
        }

        [TestCase(-1)]
        [TestCase(-1999)]
        public void CapacityThrowsIfNegative(int capacity)
        {
            Assert.Throws<ArgumentException>(() => new Gym("test", capacity), "Invalid gym capacity.");
        }

        [Test]
        public void AddAthleteTest() 
        {            
            gym.AddAthlete(new Athlete("name"));

            Assert.AreEqual(1, gym.Count);
        }

        [Test]
        public void AddAthleteThrowIfGymFull_Test()
        {
            int capacity = 1;
            var gym = new Gym("Test", capacity);
            gym.AddAthlete(new Athlete("name"));

            Assert.Throws<InvalidOperationException>(() => gym.AddAthlete(new Athlete("pesho")));
        }

        [Test]
        public void RemoveAthleteTest()
        {
            string name = "pesho";
            var gym = new Gym("Testgym", 2);
            gym.AddAthlete(new Athlete(name));
            gym.AddAthlete(new Athlete("gosho"));

            Assert.AreEqual(2, gym.Count);

            gym.RemoveAthlete(name);

            Assert.AreEqual(1, gym.Count);

            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete(name), $"The athlete {name} doesn't exist.");
        }

        [Test]
        public void RemoveAthletThrowIfInvalidName()
        {
            var gym = new Gym("Testgym", 2);
            gym.AddAthlete(new Athlete("pesho"));

            Assert.Throws<InvalidOperationException>(() => gym.RemoveAthlete("InvalidName"));
        }

        [Test]
        public void InjureAthleteTest()
        {
            string name = "pesho";
            var gym = new Gym("Testgym", 2);

            var athlete = new Athlete(name);
            gym.AddAthlete(athlete);

            gym.InjureAthlete(name);

            Assert.IsTrue(athlete.IsInjured && athlete.FullName == name);

            name = "joro";
            gym.AddAthlete(new Athlete(name));

            name = "gosho";
            var athlete2 = new Athlete(name);

            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete(name), $"The athlete {name} doesn't exist.");
        }

        [Test]
        public void InjureAthleteThrowIfTest()
        {
            var gym = new Gym("Testgym", 2);
            gym.AddAthlete(new Athlete("pesho"));

            Assert.Throws<InvalidOperationException>(() => gym.InjureAthlete("InvalidName"));
        }

        [Test]
        public void ReportTest()
        {
            var gym = new Gym("Testgym", 5);
            string name1 = "pesho";
            string name2 = "gosho";
            string name3 = "yoyo";
            var athlete1 = new Athlete(name1);
            var athlete2 = new Athlete(name2);
            var athlete3 = new Athlete(name3);
            List<Athlete> athletes = new List<Athlete> { athlete1, athlete2, athlete3 };
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            gym.InjureAthlete(name3);


            //string expectedOutput = $"Active athletes at {gym.Name}: {name1}, {name2}";
            string expectedString = $"Active athletes at {"Testgym"}: {string.Join(", ", athletes.Where(x => !x.IsInjured).Select(x => x.FullName))}";

            Assert.AreEqual(expectedString, gym.Report());
        }
    }
}
