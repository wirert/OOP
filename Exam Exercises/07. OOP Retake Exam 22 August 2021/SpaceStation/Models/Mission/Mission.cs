namespace SpaceStation.Models.Mission
{
    using System.Collections.Generic;
    using System.Linq;

    using Astronauts.Contracts;
    using Contracts;
    using Planets.Contracts;

    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var astronaut in astronauts.Where(a => a.CanBreath))
            {
                while(planet.Items.Count > 0 && astronaut.CanBreath)
                {
                    string item = planet.Items.First();
                    astronaut.Breath();
                    astronaut.Bag.Items.Add(item);
                    planet.Items.Remove(item);
                }
            }
        }
    }
}
