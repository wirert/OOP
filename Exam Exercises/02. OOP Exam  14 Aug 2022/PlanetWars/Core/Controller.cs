namespace PlanetWars.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Contracts;
    using Models.MilitaryUnits.Contracts;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Models.Weapons;
    using Models.Weapons.Contracts;
    using Utilities.Messages;
    using Repositories;
    using Repositories.Contracts;

    public class Controller : IController
    {
        private const double TrainArmyCost = 1.25;

        private IRepository<IPlanet> planets;

        public Controller()
        {
            planets = new PlanetRepository();
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = FindPlanetIfExist(planetName);

            var unit = CheckAndInstantiateUnit(unitTypeName, planet);

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = FindPlanetIfExist(planetName);

            Type type = GetTypeByName(weaponTypeName);

            if (planet.Weapons.Any(u => u.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string
                    .Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planet.Name));
            }

            IWeapon weapon = null;
            try
            {
                weapon = (IWeapon)Activator.CreateInstance(type, new object[] { destructionLevel });
            }
            catch (TargetInvocationException tiex)
            {
                throw tiex.InnerException;
            }


            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.Models.Any(p => p.Name == name))
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            planets.AddItem(new Planet(name, budget));

            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (IPlanet planet in planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().Trim();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet winner = null;
            IPlanet loser = null;
            var planet1 = planets.FindByName(planetOne);
            var planet2 = planets.FindByName(planetTwo);
            planet1.Spend(planet1.Budget / 2);
            planet2.Spend(planet2.Budget / 2);
            int result = CamparePlanetsByPower(planet1, planet2);

            if (result == 1)
            {
                winner = planet1;
                loser = planet2;
            }
            else if (result == -1)
            {
                winner = planet2;
                loser = planet1;
            }
            else
            {
                return OutputMessages.NoWinner;
            }

            double loserForcesCost = loser.Army.Sum(u => u.Cost);
            double loserWeaponsPrices = loser.Weapons.Sum(w => w.Price);

            winner.Profit(loser.Budget + loserForcesCost + loserWeaponsPrices);

            planets.RemoveItem(loser.Name);

            return string.Format(OutputMessages.WinnigTheWar, winner.Name, loser.Name);
        }

        public string SpecializeForces(string planetName)
        {
            var planet = planets.FindByName(planetName);

            if (!planet.Army.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.NoUnitsFound);
            }

            planet.Spend(TrainArmyCost);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        private IPlanet FindPlanetIfExist(string planetName)
        {
            var planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            return planet;
        }

        private IMilitaryUnit CheckAndInstantiateUnit(string unitTypeName, IPlanet planet)
        {
            var type = GetTypeByName(unitTypeName);

            if (planet.Army.Any(u => u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string
                    .Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planet.Name));
            }

            return (IMilitaryUnit)Activator.CreateInstance(type);
        }

        private Type GetTypeByName(string typeName)
        {
            Assembly assembly = typeof(StartUp).Assembly;

            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);

            if (type == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, typeName));
            }

            return type;
        }

        private int CamparePlanetsByPower(IPlanet planetOne, IPlanet planetTwo)
        {
            if (planetOne.MilitaryPower > planetTwo.MilitaryPower)
            {
                return 1;
            }
            else if (planetOne.MilitaryPower < planetTwo.MilitaryPower)
            {
                return -1;
            }
            else
            {
                if ((IsHaveNucs(planetOne) && IsHaveNucs(planetTwo))
                    || (!IsHaveNucs(planetOne) && !IsHaveNucs(planetTwo)))
                {
                    return 0;
                }
                else if (IsHaveNucs(planetOne) && !IsHaveNucs(planetTwo))
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        private bool IsHaveNucs(IPlanet planet)
            => planet.Weapons.Any(w => w.GetType().Name == typeof(NuclearWeapon).Name);


    }
}
