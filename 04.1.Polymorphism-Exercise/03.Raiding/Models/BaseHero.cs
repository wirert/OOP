namespace Raiding.Models
{
    public abstract class BaseHero
    {
        protected BaseHero(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public abstract int Power { get; }

        protected virtual string CastCurrentAbility() => $"{GetType().Name} - {this.Name} ";        

        public string CastAbility()
        {
            return CastCurrentAbility();
        }
    }
}
