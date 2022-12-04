namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AquariumsTests
    {
        private Aquarium aquarium;
        private string name;
        private int capacity;
        private Fish fish;
        private string fishName;

        [SetUp]
        public void SetUp()
        {
            name = "Test";
            capacity = 2;
            aquarium = new Aquarium(name, capacity);
            fishName = "TestFish";
            fish = new Fish(fishName);
        }

        [Test]
        public void Ctor_Test()
        {
            Assert.AreEqual(name, aquarium.Name);
            Assert.AreEqual(capacity, aquarium.Capacity);
            Assert.AreEqual(0, aquarium.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameThrowIfNullOrEmpty(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(name, 5));
        }

        [Test]
        public void CapacityThrowIfBelowZero()
        {
            int wrongCapacity = -1;
            Assert.Throws<ArgumentException>(() => new Aquarium("Some Name", wrongCapacity));
        }

        [Test]
        public void TestFishCounter()
        {
            aquarium.Add(fish);

            Assert.AreEqual(1, aquarium.Count);
        }

        [Test]
        public void AddFishThrowIfNoRoomForFishLeft()
        {
            aquarium.Add(fish);
            aquarium.Add(new Fish("fish2"));

            Assert.Throws<InvalidOperationException>(() => aquarium.Add(new Fish("fish3")));
        }

        [Test]
        public void RemoveFishThrowIfNotFoundFishName()
        {
            string invalidName = "invalidName";
            aquarium.Add(fish);

            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish(invalidName));
        }

        [Test]
        public void RemoveFishTest()
        {
            aquarium.Add(fish);

            aquarium.RemoveFish(fishName);

            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void SellFishTest()
        {
            aquarium.Add(fish);

            Assert.IsTrue(aquarium.SellFish(fishName).Available == false);
        }

        [Test]
        public void SellFishThrowIfNotFoundFishName()
        {
            string invalidName = "invalidName";
            aquarium.Add(fish);

            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish(invalidName));
        }

        [Test]
        public void ReportTest()
        {
            aquarium.Add(fish);
            aquarium.Add(new Fish("fish2"));

            string expectedOutput = $"Fish available at {name}: {fishName}, fish2";

            Assert.AreEqual(expectedOutput, aquarium.Report());
        }

    }
}
