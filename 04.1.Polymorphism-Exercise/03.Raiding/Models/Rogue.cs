namespace Raiding.Models
{
    internal class Rogue : BaseHero
    {
        public Rogue(string name) : base(name)
        { }

        public override int Power => 80;

        protected override string CastCurrentAbility() => base.CastCurrentAbility() + $"hit for {Power} damage";
    }
}
