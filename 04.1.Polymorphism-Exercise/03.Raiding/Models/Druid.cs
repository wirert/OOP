namespace Raiding.Models
{
    public class Druid : BaseHero
    {
        public Druid(string name) : base(name)
        {  }
        
        public override int Power => 80;        

        protected override string CastCurrentAbility() => base.CastCurrentAbility() + $"healed for {this.Power}";        
    }
}
