namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RobotsTests
    {
        private RobotManager manager;
        private Robot robot;
        private string robotName;
        int battary;
        int capacity;


        [SetUp]
        public void SetUp()
        {
            capacity = 2;
            manager = new RobotManager(capacity);
            robotName = "Robotnik";
            battary = 100;
            robot = new Robot(robotName, battary);
        }

        [Test]
        public void Ctor_Set_Test()
        {
            int capacity = 5;
            RobotManager manager = new RobotManager(capacity);

            Assert.AreEqual(capacity, manager.Capacity);
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void CapacityThrowIfBelowZero()
        {
            int capacity = -5;
            
            Assert.Throws<ArgumentException>(() => new RobotManager(capacity));
        }

        [Test]
        public void AddThrowIfNameExists()
        {
            manager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => manager.Add(new Robot(robotName, 40)));
        }

        [Test]
        public void AddThrowIfNoRoomForMoreRobots()
        {
            manager.Add(robot);
            manager.Add(new Robot("1", 50));
            robotName = "pesho";
            Assert.Throws<InvalidOperationException>(() => manager.Add(new Robot(robotName, 40)));
        }

        [Test]
        public void AddRobotAddsItToCollection()
        {
            manager.Add(robot);
            Assert.AreEqual(1, manager.Count);
        }

        [Test]
        public void RemoveThrowIfNameNotFound()
        {
            manager.Add(robot);
            robotName = "invalidName";
            Assert.Throws<InvalidOperationException>(() => manager.Remove(robotName));
        }

        [Test]
        public void RemoveRobotRemovesItFromCollection()
        {
            manager.Add(robot);
            manager.Remove(robotName);
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void WorkThrowIfNameNotFound()
        {
            manager.Add(robot);
            robotName = "invalidName";
            Assert.Throws<InvalidOperationException>(() => manager.Work(robotName, "job", battary - 20));
        }

        [Test]
        public void WorkThrowIfNotEnoughBattary()
        {
            manager.Add(robot);
           
            Assert.Throws<InvalidOperationException>(() => manager.Work(robotName, "job", battary + 1));
        }

        [Test]
        public void WorkReduceBattary()
        {
            manager.Add(robot);

            manager.Work(robotName, "job", battary - 20);

            Assert.AreEqual(battary - (battary - 20), robot.Battery);
        }

        [Test]
        public void ChargeThrowIfNameNotFound()
        {
            manager.Add(robot);
            robotName = "invalidName";
            Assert.Throws<InvalidOperationException>(() => manager.Charge(robotName));
        }

        [Test]
        public void ChargeSetBattaryToMaxValue()
        {
            manager.Add(robot);

            manager.Work(robotName, "job", battary - 20);

            manager.Charge(robotName);

            Assert.AreEqual(battary, robot.Battery);
        }
    }
}
