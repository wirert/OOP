using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    [TestFixture]
    public class PlanetTest
    {
        private Planet planet;

        [SetUp]
        public void SetUp()
        {
            planet = new Planet("Test", 20);
        }

        [Test]
        public void TestCtor()
        {
            string name = "Test";
            double budget = 23.4;            

            Planet planet = new Planet(name,budget);
            Assert.AreEqual(name, planet.Name);
            Assert.AreEqual(budget, planet.Budget);
            Assert.AreEqual(0, planet.Weapons.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void NameThrowExceptionIfNullOrEmpty(string name)
        {
            Assert.Throws<ArgumentException>(() => new Planet(name, 20) , "Invalid planet Name");
        }

        [TestCase(-1)]
        [TestCase(-30000)]
        public void BudgetThrowExceptionWhenBelowZero(int budget)
        {
            Assert.Throws<ArgumentException>(() => new Planet("Test", budget), "Invalid planet Name");
        }

        [Test]
        public void MilitaryPowerRatioReturnSumOfWeaponsDestrLevels()
        {            
            planet.AddWeapon(new Weapon("1", 5, 5));
            planet.AddWeapon(new Weapon("2", 5, 12));
            double expectedPower = 5 + 12;

            Assert.AreEqual(expectedPower, planet.MilitaryPowerRatio);
        }

        [Test]
        public void ProfitShoudAddToBudget()
        {
            planet.Profit(10);
            double expectedBudget = 10 + 20;
            Assert.AreEqual(expectedBudget, planet.Budget);
        }

        [Test]
        public void SpendFundsShoudSubtractFromBudget()
        {
            planet.SpendFunds(10);
            double expectedBudget =  20 - 10;
            Assert.AreEqual(expectedBudget, planet.Budget);
        }

        [Test]
        public void SpendFundsShoudThrowExceptionWhenMoreThanBudget()
        {
            Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(30)
            , "Not enough funds to finalize the deal.");
        }

        [Test]
        public void AddingWeaponWithSameNameShouldThrowException()
        {
            Weapon weapon1 = new Weapon("Test", 5, 4);
            Weapon weapon2 = new Weapon("Test", 3, 6);
            planet.AddWeapon(weapon1);

            Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon2)
            , $"There is already a {weapon2.Name} weapon.");
        }
        [Test]
        public void TestRemoveWeapon()
        {
            Weapon weapon = new Weapon("Test", 5, 4);
            planet.AddWeapon(weapon);
            planet.RemoveWeapon("Test");
            Assert.AreEqual(0, planet.Weapons.Count);
        }

        [Test]
        public void TestWeaponUpgrade()
        {
            Weapon weapon = new Weapon("Test", 5, 4);
            planet.AddWeapon(weapon);
            planet.UpgradeWeapon("Test");
            int expectedDestrLevel = 4 + 1;

            Assert.AreEqual(expectedDestrLevel, weapon.DestructionLevel);
        }

        [Test]
        public void WeaponUpgradeThrowsExeptionWhenNameNotFound()
        {
            Weapon weapon = new Weapon("Test", 5, 4);
            planet.AddWeapon(weapon);
            string weaponName = "T";

            Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon(weaponName), $"{weaponName} does not exist in the weapon repository of {planet.Name}");
        }

        [Test]
        public void DestructOpponentTest()
        {
            Weapon weapon1 = new Weapon("1", 5, 4);
            Weapon weapon2 = new Weapon("2", 3, 6);
            Planet opponent = new Planet("Oponent", 20);
            planet.AddWeapon(weapon1);
            opponent.AddWeapon(weapon2);

            Assert.AreEqual($"{planet.Name} is destructed!", opponent.DestructOpponent(planet));

            Assert.Throws<InvalidOperationException>(() => planet.DestructOpponent(opponent)
            , $"{opponent.Name} is too strong to declare war to!");
        }

    }
}
