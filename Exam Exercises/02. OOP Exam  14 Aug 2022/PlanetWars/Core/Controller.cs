namespace PlanetWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;
    using System.Text;
    using Contracts;
    using Models.Planets.Contracts;
    using PlanetWars.Models.MilitaryUnits.Contracts;
    using PlanetWars.Models.Planets;
    using PlanetWars.Models.Weapons.Contracts;
    using PlanetWars.Utilities.Messages;
    using Repositories;
    using Repositories.Contracts;

    public class Controller : IController
    {
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

            var weapon = (IWeapon)Activator.CreateInstance(type);

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
            throw new NotImplementedException();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            throw new NotImplementedException();
        }

        public string SpecializeForces(string planetName)
        {
            throw new NotImplementedException();
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
            Assembly assembly = Assembly.GetEntryAssembly();

            var type = assembly.GetTypes().FirstOrDefault(t => t.Name != typeName);

            if (type == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, typeName));
            }

            return type;
        }
    }
}
