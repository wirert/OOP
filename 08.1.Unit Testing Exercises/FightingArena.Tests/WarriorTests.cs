namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private string name;
        private int damage;
        private int hp;

        [SetUp] public void SetUp()
        {
            name = "Pesho";
            damage = 20;
            hp= 100;
        }


        [TestCase("     ")]
        [TestCase(null)]
        [TestCase("")]
        public void NameSetterThrowExceptionIfNullOrWhiteSpace(string newName)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(newName, damage,hp)
            , "Name should not be empty or whitespace!");

            var warrior = new Warrior(name, damage, hp);
            Assert.AreEqual(this.name, warrior.Name);
        }

        [TestCase(0)]
        [TestCase(-400)]
        public void DamageSetterThrowExceptionIfBelowOrEqualZero(int damage)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, hp)
            , "Damage value should be positive!");

            var warrior = new Warrior(name, this.damage,hp);
            Assert.AreEqual(this.damage, warrior.Damage);
        }

        [TestCase(-1)]
        [TestCase(-400)]
        public void HPShouldNotBeNegative(int newHp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, newHp)
            , "HP should not be negative!");

            var warrior = new Warrior(name, damage, hp);
            Assert.AreEqual(this.hp, warrior.HP);
        }

        [TestCase(30)]
        [TestCase(1)]
        public void ThrowExceptionIfTryToAttackWithHPLessThanMinNeeded(int newHp)
        {
            Warrior warrior1 = new Warrior(name, damage, newHp);
            Warrior warrior2 = new Warrior(name, damage, hp);
            
            Assert.Throws<InvalidOperationException>(() => warrior1.Attack(warrior2)
            , "Your HP is too low in order to attack other warriors!");
        }

        [TestCase(30)]
        [TestCase(1)]
        public void ThrowExceptionIfTryToAttackWarriorWithHPLessThanMinNeeded(int newHp)
        {
            Warrior warrior1 = new Warrior(name, damage, newHp);
            Warrior warrior2 = new Warrior(name, damage, hp);

            Assert.Throws<InvalidOperationException>(() => warrior2.Attack(warrior1)
            , "Enemy HP must be greater than 30 in order to attack him!");
        }

        [Test]
        public void ThrowExceptionIfTryToAttackStrongerEnemy()
        {
            Warrior w1 = new Warrior(name, damage, 50);
            Warrior w2 = new Warrior(name, 100, hp);

            Assert.Throws<InvalidOperationException>(() => w1.Attack(w2)
            , "You are trying to attack too strong enemy");
        }

        [Test]
        public void SucessAttackWithKill()
        {
            int w1Damage = 50;
            int w2Damage = 50;
            int w1Hp = 100;
            int w2Hp = 40;
            Warrior w1 = new Warrior(name, w1Damage, w1Hp);
            Warrior w2 = new Warrior("Gosho", w2Damage, w2Hp);

            w1.Attack(w2);

            Assert.AreEqual(w1Hp - w2Damage, w1.HP);
            Assert.AreEqual(0, w2.HP);
        }

        [Test]
        public void SucessAttackWithOutKill()
        {
            int w1Damage = 50;
            int w2Damage = 50;
            int w1Hp = 100;
            int w2Hp = 60;
            Warrior w1 = new Warrior(name, w1Damage, w1Hp);
            Warrior w2 = new Warrior("Gosho", w2Damage, w2Hp);

            w1.Attack(w2);

            Assert.AreEqual(w1Hp - w2Damage, w1.HP);
            Assert.AreEqual(w2Hp - w1Damage, w2.HP);
        }
    }
}