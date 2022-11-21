using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private const int Health = 10;
        private const int Experience = 10;
        private Dummy dummy;
        private Dummy deadDummy;

        [SetUp]
        public void TestInitialize()
        {
            dummy = new Dummy(Health, Experience);
            deadDummy = new Dummy(-5, Experience);
        }

        [Test]
        public void DummyLosesHealthWhenAttacked()
        {
            dummy.TakeAttack(Health);

            Assert.AreEqual(dummy.Health, 0);
        }

        [Test]
        public void DeadDummyThrowExeptionWhenAttacked()
        {
            dummy.TakeAttack(Health);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(Health), "Dummy health is zero");
        }

        [Test]
        public void DeadDummyShouldGiveExperience()
        {
            Assert.AreEqual(10, deadDummy.GiveExperience());
        }

        [Test]
        public void AliveDummyCanNotGiveExperience()
        {
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(), "Alive Dummy can't give experience");
        }
    }
}