namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Planets.Contracts;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private ICollection<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.planets as IReadOnlyCollection<IPlanet>;

        public void AddItem(IPlanet model) => this.planets.Add(model);
        

        public IPlanet FindByName(string name)
         => Models.FirstOrDefault(w => w.Name == name);

        public bool RemoveItem(string name)
        {
            IPlanet planet = this.FindByName(name);

            if (planet != null)
            {
                planets.Remove(planet);
                return true;
            }

            return false;
        }
    }
}
