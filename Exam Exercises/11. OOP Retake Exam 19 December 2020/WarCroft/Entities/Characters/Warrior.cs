namespace WarCroft.Entities.Characters
{
    using Contracts;
    using Inventory;
    using System;
    using WarCroft.Constants;

    public class Warrior : Character, IAttacker
    {
        private const double BASE_HEALTH = 100;
        private const double BASE_ARMOR = 50;
        private const double BASE_ABILITY_POINTS = 40;

        public Warrior(string name)
            : base(name, BASE_HEALTH, BASE_ARMOR, BASE_ABILITY_POINTS, new Satchel())
        {
        }

        public void Attack(Character character)
        {
            this.EnsureAlive();
            character.EnsureAlive();

            if (character == this)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterAttacksSelf);
            }

            character.TakeDamage(this.AbilityPoints);
        }
    }
}
