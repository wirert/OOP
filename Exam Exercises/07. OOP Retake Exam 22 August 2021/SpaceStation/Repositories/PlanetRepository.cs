namespace SpaceStation.Repositories
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
            planets = new List<IPlanet>();            
        }

        public IReadOnlyCollection<IPlanet> Models => (IReadOnlyCollection<IPlanet>)planets;

        public void Add(IPlanet model) => planets.Add(model);

        public IPlanet FindByName(string name) => planets.FirstOrDefault(p => p.Name == name);

        public bool Remove(IPlanet model) => planets.Remove(model);
    }
}
