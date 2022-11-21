using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private const int Attack = 10;
        private const int Durability = 10;
        private const int Health = 10;
        private const int Experience = 10;

        private Axe axe;
        private Dummy dummy;

        [SetUp]
        public void TestInitialize()
        {
            axe = new Axe(Attack, Durability);
            dummy = new Dummy(Health, Experience);
        }

        [Test]
        public void AxeLosesDurabilityAfterAttack()
        {
            axe.Attack(dummy);

            Assert.AreEqual(axe.DurabilityPoints, 9);
        }

        
        [Test]
        public void ThrowExceptionWhenDurabilityGoesBelowZero()
        {
            Axe axe1 = new Axe(10, 0);

            Assert.Throws<InvalidOperationException>(() =>
            {
                axe1.Attack(dummy);
            }, "Axe is broken.");
        }

        [Test]
        public void DurabilityGetterShouldReturnActualValueOfDurability()
        {
            int expectedDurability = 10;
            int actualDurability = axe.DurabilityPoints;

            Assert.AreEqual (expectedDurability, actualDurability);

            axe.Attack(dummy);
            expectedDurability--;
            actualDurability = axe.DurabilityPoints;
            Assert.AreEqual(expectedDurability, actualDurability);
        }

        [Test]
        public void AttackPointsGetterShouldReturnActualValueOfAttackPoints()
        {
            int expectedAttackPoints = 10;
            int actualAttackPoints = axe.AttackPoints;

            Assert.AreEqual(expectedAttackPoints, actualAttackPoints);
        }
    }
}