namespace _03BarracksFactory.Models.Units
{
    internal class Gunner : Unit
    {
        private const int DefaultHealth = 20;
        private const int DefaultAttackDamage = 20;

        protected Gunner() : base(DefaultHealth, DefaultAttackDamage)
        {
        }
    }
}
