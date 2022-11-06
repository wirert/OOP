namespace Raiding.Models
{
    internal class Warrior : BaseHero
    {
        public Warrior(string name) : base(name)
        { }

        public override int Power => 100;

        protected override string CastCurrentAbility() => base.CastCurrentAbility() + $"hit for {Power} damage";
    }
}
