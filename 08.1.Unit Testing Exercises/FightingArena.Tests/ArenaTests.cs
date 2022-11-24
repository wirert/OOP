namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Xml.Linq;

    [TestFixture]
    public class ArenaTests
    {

        [Test]
        public void TestArenaConstructor()
        {
            Arena arena = new Arena();

            Assert.IsNotNull(arena);
            Assert.AreEqual(0, arena.Count);
        }

        [Test]
        public void EnrollWarrorsWithSameNamesThrowExcepton()
        {
            Arena arena = new Arena();
            arena.Enroll(new Warrior("pesho", 50, 50));

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("pesho", 10, 80))
            , "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void FightThrowExceptionIfAnyFighterIsNotEnrolledInTheArena()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Gosho", 40, 100);
            Arena arena = new Arena();
            arena.Enroll(w1);
            Assert.Throws<InvalidOperationException>(() => arena.Fight("Pesho", "Gosho")
            , "There is no fighter with name Gosho enrolled for the fights!");
            Assert.Throws<InvalidOperationException>(() => arena.Fight("Gosho", "Pesho")
           , "There is no fighter with name Gosho enrolled for the fights!");
        }

        [Test]
        public void SuccessfulAttackCollsWarriorAttackMethod()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Gosho", 40, 100);
            Arena arena = new Arena();
            arena.Enroll(w1);
            arena.Enroll(w2);

            arena.Fight("Gosho", "Pesho");

            Assert.AreEqual(60, w1.HP);
            Assert.AreEqual(50, w2.HP);
        }

        [Test]
        public void CheckPropWarriors()
        {
            Warrior w1 = new Warrior("Pesho", 50, 100);
            Warrior w2 = new Warrior("Gosho", 40, 100);
            Arena arena = new Arena();
            arena.Enroll(w1);
            arena.Enroll(w2);

            Assert.AreEqual(2, arena.Warriors.Count);
        }
    }
}
