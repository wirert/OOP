namespace _03BarracksFactory.Models.Units
{
    public class Horseman : Unit
    {
        private const int DefaultHealth = 50;
        private const int DefaultAttackDamage = 10;

        protected Horseman() : base(DefaultHealth, DefaultAttackDamage)
        {
        }
    }
}
