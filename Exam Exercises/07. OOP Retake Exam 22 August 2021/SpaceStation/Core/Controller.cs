namespace SpaceStation.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Astronauts;
    using Models.Astronauts.Contracts;
    using Models.Mission;
    using Models.Mission.Contracts;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private IRepository<IAstronaut> astronautRepository;
        private IRepository<IPlanet> planetRepository;
        private int exploredPlanets;

        public Controller()
        {
            astronautRepository = new AstronautRepository();
            planetRepository = new PlanetRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronault = null;

            if (type == "Biologist")
            {
                astronault = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronault = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronault = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            astronautRepository.Add(astronault);

            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);

            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            planetRepository.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronaut = astronautRepository.FindByName(astronautName);

            if (astronaut == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            astronautRepository.Remove(astronaut);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }

        public string ExplorePlanet(string planetName)
        {
            var planetToExplore = planetRepository.FindByName(planetName);
            var suitableAstronauts = astronautRepository.Models.Where(a => a.Oxygen > 60 && a.CanBreath).ToList();

            if (suitableAstronauts.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IMission mission = new Mission();

            mission.Explore(planetToExplore, suitableAstronauts);

            exploredPlanets++;
            int deadAstronauts = suitableAstronauts.Where(a => a.CanBreath == false).Count();

            return string.Format(OutputMessages.PlanetExplored, planetName, deadAstronauts);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{exploredPlanets} planets were explored!")
                .AppendLine("Astronauts info:");

            foreach (var astronaut in astronautRepository.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}")
                    .AppendLine($"Oxygen: {astronaut.Oxygen}");

                string itemsInBag = astronaut.Bag.Items.Count == 0 ? "none" : string.Join(", ", astronaut.Bag.Items);
                sb.AppendLine($"Bag items: {itemsInBag}");
            }

            return sb.ToString().Trim();
        }
    }
}
