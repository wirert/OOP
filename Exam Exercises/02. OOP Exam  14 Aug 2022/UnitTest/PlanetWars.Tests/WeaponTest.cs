using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    [TestFixture]
    public class WeaponTest
    {
        [Test]
        public void WeaponConstructorSetTest()
        {
            string name = "Test";
            double price = 2.4;
            int destrLevel = 4;
            Weapon weapon = new Weapon(name, price, destrLevel);

            Assert.AreEqual(name, weapon.Name);
            Assert.AreEqual(price, weapon.Price);
            Assert.AreEqual(destrLevel, weapon.DestructionLevel);
        }

        [TestCase(-1)]
        [TestCase(-1000)]
        public void WeaponPriceShouldBePositiveNumberOrZero(double price)
        {
            Assert.Throws<ArgumentException>(() => new Weapon("Test", price, 4), "Price cannot be negative.");
        }

        [Test]
        public void IncreaseDestructionLevelBy_1()
        {
            int destrLevel = 1;
            Weapon weapon = new Weapon("T", 2, destrLevel);
            weapon.IncreaseDestructionLevel();
            Assert.AreEqual(destrLevel + 1, weapon.DestructionLevel);
        }

        [TestCase(11)]
        [TestCase(200)]
        public void WeaponIsNuclearWithDestructionMoreThan_10(int destrLevel)
        {
            Weapon weapon = new Weapon("T", 2, destrLevel);

            Assert.IsTrue(weapon.IsNuclear);
        }
    }
}
