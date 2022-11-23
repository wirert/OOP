namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        string make;
        string model;
        double consumption;
        double fuelCapacity;
        Car car;

        [SetUp]
        public void SetUp()
        {
            make = "Saab";
            model = "9-5";
            consumption = 10;
            fuelCapacity = 50;

            car = new Car(make, model, consumption, fuelCapacity);
        }

        [Test]
        public void ConstructorShouldSetValidValuesViaProps()
        {
            Assert.IsNotNull(car);
            Assert.AreEqual(0, car.FuelAmount);
            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(consumption, car.FuelConsumption);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
        }

        [TestCase("")]
        [TestCase(null)]
        public void NullOrEmptyMakeShouldThrowException(string testMake)
        {
            make = testMake;
            Assert.Throws<ArgumentException>(() => new Car(make, model, consumption, fuelCapacity)
            , "Make cannot be null or empty!");
        }

        [TestCase("")]
        [TestCase(null)]
        public void NullOrEmptyModelShouldThrowException(string testModel)
        {
            model = testModel;
            Assert.Throws<ArgumentException>(() => new Car(make, model, consumption, fuelCapacity)
            , "Model cannot be null or empty!");
        }

        [TestCase(0)]
        [TestCase(-29)]
        public void FuelConsumptionShouldBeAboveZero(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity)
            , "Fuel consumption cannot be zero or negative!");
        }

        [TestCase(0)]
        [TestCase(-29)]
        public void FuelCapacityShouldBeAboveZero(double capacity)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model, consumption, capacity)
            , "Fuel capacity cannot be zero or negative!");
        }

        [TestCase(0)]
        [TestCase(-29)]
        public void RefuelFuelCannotBeNegativeOrZero(double fuelToRefuel)
        {
            Assert.Throws<ArgumentException>(() => car.Refuel(fuelToRefuel)
            , "Fuel amount cannot be zero or negative!");
        }

        [TestCase(51)]
        [TestCase(100)]
        public void FuelCannotBeMoreThanFuelCapacity(double fuelToRefuel)
        {
            car.Refuel(fuelToRefuel);
            Assert.AreEqual(fuelCapacity, car.FuelAmount);
        }

        //public void FuelAmountCannotBeNegative()
        //{

        //}

        [TestCase(1)]
        [TestCase(100)]
        public void DriveCarShouldThrowExceptionIfFuelIsNotEnough(double distance)
        {
            Assert.Throws<InvalidOperationException>(() => car.Drive(distance)
            , "You don't have enough fuel to drive!");
        }

        [TestCase(1, 50)]
        [TestCase(0, 50)]
        [TestCase(100, 20)]
        public void DriveCarShouldReduceFuel(double distance, double fuel)
        {
            double neededFuel = distance * consumption / 100;
            car.Refuel(fuel);
            car.Drive(distance);

            Assert.AreEqual(fuel - neededFuel, car.FuelAmount);
        }
    }
}