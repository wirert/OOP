namespace _03BarracksFactory.Models.Units
{
    public class Gunner : Unit
    {
        private const int DefaultHealth = 20;
        private const int DefaultAttackDamage = 20;

        protected Gunner() : base(DefaultHealth, DefaultAttackDamage)
        {
        }
    }
}
