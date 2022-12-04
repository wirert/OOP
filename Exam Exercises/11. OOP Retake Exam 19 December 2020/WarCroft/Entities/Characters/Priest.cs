namespace WarCroft.Entities.Characters
{
    using Contracts;
    using Inventory;

    public class Priest : Character, IHealer
    {
        private const double BASE_HEALTH = 50;
        private const double BASE_ARMOR = 25;
        private const double BASE_ABILITY_POINTS = 40;

        public Priest(string name)
            : base(name, BASE_HEALTH, BASE_ARMOR, BASE_ABILITY_POINTS, new Backpack())
        {
        }

        public void Heal(Character character)
        {
            this.EnsureAlive();
            character.EnsureAlive();

            character.Health += this.AbilityPoints;
        }
    }
}
