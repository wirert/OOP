namespace Raiding.Models
{
    public class Paladin : BaseHero
    {
        public Paladin(string name) : base(name)
        { }

        public override int Power => 100;

        protected override string CastCurrentAbility() => base.CastCurrentAbility() + $"healed for {this.Power}";
    }
}
