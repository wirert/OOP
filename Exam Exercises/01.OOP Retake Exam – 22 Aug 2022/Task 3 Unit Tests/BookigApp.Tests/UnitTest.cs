using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        private Hotel hotel;

        [SetUp]
        public void Setup()
        {
            hotel = new Hotel("Test", 1);
        }

        [TestCase("T", 1)]
        [TestCase("Long Long    Long     Long       Long  dfafadsdfd name", 5)]
        [TestCase("Test hotel", 3)]
        public void ConstructorShouldSetStateCorrectly(string name, int category)
        {

            Hotel hotel = new Hotel(name, category);

            Assert.AreEqual(name, hotel.FullName);
            Assert.AreEqual(category, hotel.Category);
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Rooms.Count);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void HotelNameShouldThrowExceptionIfNullOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(name, 3));
        }

        [TestCase(-100)]
        [TestCase(0)]
        [TestCase(6)]
        public void Category_Should_Be_Between_1_And_5(int category)
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Test", category));
        }

        [Test]
        public void AddRoomShouldAddToCollectionOfRooms()
        {
            hotel.AddRoom(new Room(1, 1));

            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void AdultsShouldBePositiveNumber(int adults)
        {
            hotel.AddRoom(new Room(1, 1));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(adults, 2, 1, 200));
        }

        [TestCase(-1)]
        [TestCase(-50)]
        public void ChildrenShouldBePositiveNumberOrZero(int children)
        {
            hotel.AddRoom(new Room(1, 1));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, children, 1, 200));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void ResidenceDurationShouldBeMoreThan_0(int duration)
        {
            hotel.AddRoom(new Room(1, 1));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 2, duration, 200));
        }

        [Test]
        public void ToBookRoomThereShouldHaveASuitableRoom()
        {
            Room room1 = new Room(2, 10);
            Room room2 = new Room(4, 20);
            hotel.AddRoom(room1);
            int adults = 1;
            int children = 1;
            int duration = 1;
            double money = 50;
            double expectedTurnover = duration * room1.PricePerNight;
            hotel.BookRoom(adults, children, duration, money);
           
            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(expectedTurnover, hotel.Turnover);

            adults = 5;
            hotel.BookRoom(adults, children, duration, money);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(expectedTurnover, hotel.Turnover);

            adults = 1;
            money = 5;

            hotel.BookRoom(adults, children, duration, money);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(expectedTurnover, hotel.Turnover);
        }
    }
}