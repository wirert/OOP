using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests
{
    public class Tests
    {
        private const string MANUFACTURER = "intel";
        private const string MODEL = "pentium";

        private ComputerManager manager;
        private Computer computer;

        [SetUp]
        public void Setup()
        {
            manager = new ComputerManager();
            computer = new Computer(MANUFACTURER, MODEL, 100m);
        }

        [Test]
        public void Ctor_ShouldSetEmptyCollection()
        {
            Assert.IsTrue(manager.Count == 0);
            Assert.IsTrue(manager.Computers.Count == 0);
        }

        [Test]
        public void AddComputer_ShouldAddToCollection()
        {
            manager.AddComputer(computer);

            Assert.IsTrue(manager.Computers.Count == 1);
            Assert.IsTrue(manager.Count == 1);
            Assert.AreEqual(computer, manager.Computers.First());
        }

        [Test]
        public void AddComputer_ShouldThrowIfAddNull()
        {
            Assert.Throws<ArgumentNullException>(() => manager.AddComputer(null));
        }

        [Test]
        public void AddComputer_ShouldThrowIfAlreadyInCollectonComputerWithSameManufacturerAndModel()
        {
            manager.AddComputer(computer);

            decimal newPrice = 300m;

            var newComputer = new Computer(MANUFACTURER, MODEL, newPrice);

            Assert.Throws<ArgumentException>(() => manager.AddComputer(newComputer));
            Assert.Throws<ArgumentException>(() => manager.AddComputer(computer));
        }

        [Test]
        public void GetComputer_ShouldThrowIfManufacturerIsNull()
        {
            manager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(() => manager.GetComputer(null, MODEL));
        }

        [Test]
        public void GetComputer_ShouldThrowIfComputerNotFound()
        {
            manager.AddComputer(computer);

            string invalidManufacturer = "invalid";
            string invalidModel = "invalid";

            Assert.Throws<ArgumentException>(() => manager.GetComputer(MANUFACTURER, invalidModel));
            Assert.Throws<ArgumentException>(() => manager.GetComputer(invalidManufacturer, MODEL));
            Assert.Throws<ArgumentException>(() => manager.GetComputer(invalidManufacturer, invalidModel));
        }

        [Test]
        public void GetComputer_ShouldThrowIfModelIsNull()
        {
            manager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(() => manager.GetComputer(MANUFACTURER, null));
        }

        [Test]
        public void GetComputer_ShouldReturnCorectComputer()
        {
            manager.AddComputer(computer);

            Assert.AreEqual(computer, manager.GetComputer(MANUFACTURER, MODEL));
        }

        [Test]
        public void RemoveComputer_Test()
        {
            manager.AddComputer(computer);

            Assert.AreEqual(computer, manager.RemoveComputer(MANUFACTURER, MODEL));
            Assert.AreEqual(0, manager.Computers.Count);
        }

        [Test]
        public void RemoveComputer_ThrowsForNonExistentComputerParameters()
        {
            manager.AddComputer(computer);

            Assert.Throws<ArgumentException>(() => manager.RemoveComputer(MANUFACTURER, "INVALID model"));
            Assert.Throws<ArgumentException>(() => manager.RemoveComputer("INVALID manufacturer", MODEL));
        }

        [Test]
        public void GetComputersByManufacturer_ShouldThrowIfManufacturerIsNull()
        {
            manager.AddComputer(computer);

            Assert.Throws<ArgumentNullException>(() => manager.GetComputersByManufacturer(null));
        }

        [Test]
        public void GetComputersByManufacturer_ShouldReturnCorectComputers()
        {
            manager.AddComputer(computer);

            Assert.AreEqual(computer, manager.GetComputersByManufacturer(MANUFACTURER).First());

            var computer2 = new Computer(MANUFACTURER, "newModel", 400m);
            manager.AddComputer(computer2);
            manager.AddComputer(new Computer("AnotherManufacturer", MODEL, 50m));

            var expectedComputers = new List<Computer> { computer, computer2 };

            Assert.AreEqual(expectedComputers, manager.GetComputersByManufacturer(MANUFACTURER));
        }

        [Test]
        public void GetComputersByManufacturer_ReturnEmptyCollectionIfManufacturorNotFound()
        {
            manager.AddComputer(computer);
            manager.AddComputer(new Computer("AnotherManufacturer", MODEL, 50m));
            string invalidManufactorer = "invalid";

            var result = manager.GetComputersByManufacturer(invalidManufactorer);

            Assert.IsEmpty(result);
        }
    }
}