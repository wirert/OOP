using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void CtorTest()
        {
            int capacity = 5;
            Shop shop = new Shop(capacity);

            Assert.AreEqual(capacity, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }

        [TestCase(-1)]
        [TestCase(-1000)]
        public void CapacityShouldBePositiveOrZero(int capacity)
        {
            Assert.Throws<ArgumentException>(() => new Shop(capacity), "Invalid capacity.");
        }

        [Test]
        public void AddPhoneShouldThrowIfSameModel()
        {
            Shop shop = new Shop(5);
            Smartphone phone1 = new Smartphone("Test", 5);
            Smartphone phone2 = new Smartphone("Test", 4);

            shop.Add(phone1);

            Assert.Throws<InvalidOperationException>(() => shop.Add(phone2), $"The phone model {phone1.ModelName} already exist.");
        }

        [Test]
        public void AddPhoneShouldThrowIfNoCapacity()
        {
            Shop shop = new Shop(1);
            Smartphone phone1 = new Smartphone("Test", 5);
            Smartphone phone2 = new Smartphone("Test1", 4);

            shop.Add(phone1);

            Assert.Throws<InvalidOperationException>(() => shop.Add(phone2), "The shop is full.");
        }

        [Test]
        public void RemovePhoneTest() 
        {
            Shop shop = new Shop(1);
            Smartphone phone = new Smartphone("Test", 5);

            shop.Add(phone);
            string invalidModelPhone = "Invalid";

            Assert.Throws<InvalidOperationException>(() => shop.Remove(invalidModelPhone), $"The phone model {invalidModelPhone} doesn't exist.");

            shop.Remove(phone.ModelName);

            Assert.AreEqual(0,shop.Count);
        }

        [Test]
        public void TestPhoneTest()
        {
            Shop shop = new Shop(5);
            Smartphone phone = new Smartphone("Test", 5);
            Smartphone phone1 = new Smartphone("invalid", 5);

            shop.Add(phone);

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone(phone1.ModelName, 5), $"The phone model {phone1.ModelName} doesn't exist.");

            Assert.Throws<InvalidOperationException>(() => shop.TestPhone(phone.ModelName, 6), $"The phone model {phone.ModelName} is low on batery.");

            shop.TestPhone(phone.ModelName, 3);
            int expectedCharge = 5 - 3;

            Assert.AreEqual(expectedCharge, phone.CurrentBateryCharge);
        }

        [Test]
        public void ChargePhoneTest() 
        {
            Shop shop = new Shop(5);
            Smartphone phone = new Smartphone("Test", 5);

            string invalidPhoneName = "Invalid";

            shop.Add(phone);
            shop.TestPhone(phone.ModelName, 4);

            shop.ChargePhone(phone.ModelName);

            Assert.AreEqual(5, phone.CurrentBateryCharge);

            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone(invalidPhoneName), $"The phone model {invalidPhoneName} doesn't exist.");


        }
    }
}