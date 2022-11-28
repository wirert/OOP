using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        [TestFixture]
        public class RepairsShopTests
        {
            [Test]
            public void CtorShouldSetParameters()
            {
                string name = "Test";
                int mechanics = 5;

                Garage garage = new Garage(name, mechanics);

                Assert.AreEqual(name, garage.Name);
                Assert.AreEqual(mechanics, garage.MechanicsAvailable);
                Assert.AreEqual(0, garage.CarsInGarage);
            }

            [TestCase(null)]
            [TestCase("")]
            public void NullOrEmptyNameShouldThrowException(string name)
            {
                Assert.Throws<ArgumentNullException>(() => new Garage(name, 5), nameof(name), "Invalid garage name.");
            }

            [TestCase(0)]
            [TestCase(-1)]
            [TestCase(-100)]
            public void MechanicsMustBeMoreThanZero(int mechanics)
            {
                Assert.Throws<ArgumentException>(() => new Garage("test", mechanics)
                , "At least one mechanic must work in the garage.");
            }

            [Test]
            public void AddCarShouldThrowExceptionIfNoAvailableMechanics()
            {
                Garage garage = new Garage("test", 1);
                Car car1 = new Car("1", 1);
                Car car2 = new Car("2", 2);
                garage.AddCar(car1);

                Assert.Throws<InvalidOperationException>(() => garage.AddCar(car2), "No mechanic available.");
            }

            [Test]
            public void CarFixShouldFixCarAndThrowExceptionIfCarNotFound()
            {
                Garage garage = new Garage("test", 1);
                string model = "test";
                int issues = 2;
                Car car = new Car(model, issues);
                garage.AddCar(car);

                garage.FixCar(model);
                Assert.AreEqual(0, car.NumberOfIssues);

                model = "test1";
                Assert.Throws<InvalidOperationException>(() => garage.FixCar(model), $"The car {model} doesn't exist.");
            }

            [Test]
            public void RemoveFixedCarTest()
            {
                Garage garage = new Garage("test", 4);
                Car car1 = new Car("1", 1);
                Car car2 = new Car("2", 2);
                garage.AddCar(car1);
                garage.AddCar(car2);

                Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar(), "No fixed cars available.");

                garage.FixCar("1");
                garage.RemoveFixedCar();    

                Assert.AreEqual(1, garage.CarsInGarage);
            }


            [Test]
            public void TestReport()
            {
                Garage garage = new Garage("test", 4);
                Car car1 = new Car("model1", 1);
                Car car2 = new Car("model2", 2);
                garage.AddCar(car1);
                garage.AddCar(car2);

                string expectedReport = $"There are 2 which are not fixed: model1, model2.";

                Assert.AreEqual(expectedReport, garage.Report());
            }
        }
    }
}