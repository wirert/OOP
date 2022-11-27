namespace Heroes.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Contracts;
    using Models.Map;
    using Repositories;
    using System.Reflection;
    using Utilities.Messages;

    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = heroes.FindByName(heroName);
            if (hero == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroDoesNotExist, heroName));
            }

            var weapon = weapons.FindByName(weaponName);
            if (weapon == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponDoesNotExist, weaponName));
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroAlreadyHaveWeapon, heroName));
            }

            hero.AddWeapon(weapon);

            return string.Format(OutputMessages.AddedWeaponToHero, heroName, weapon.GetType().Name.ToLower());
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.Models.Any(h => h.Name == name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.HeroNameAlreadyExist, name));
            }            

            Type heroType = GetTypeByName(type);

            if (heroType == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidHeroType);
            }

            IHero hero = null;
            try
            {
                hero = (IHero)Activator.CreateInstance(heroType, new object[] { name, health, armour });
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;                
            }            

            heroes.Add(hero);

            return type == "Knight" ? string.Format(OutputMessages.AddedKnight, name)
                : string.Format(OutputMessages.AddedBarbarian, name);
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.Models.Any(w => w.Name == name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyExist, name));
            }

            Type weaponType =  GetTypeByName(type);

            if (weaponType == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidWeaponType);
            }

            IWeapon weapon = null;

            try
            {
                weapon = (IWeapon)Activator.CreateInstance(weaponType, new object[] { name, durability });
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }

            weapons.Add(weapon);

            return string.Format(OutputMessages.AddedWeapon, type.ToLower(), name);
        }

        public string StartBattle()
        {
            Map map = new Map();
            var heroesToFight = heroes.Models.Where(h => h.IsAlive && h.Weapon != null).ToList();

            return map.Fight(heroesToFight);
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var hero in heroes.Models
                .OrderBy(h => h.GetType().Name)
                .ThenByDescending(h => h.Health)
                .ThenBy(h => h.Name))
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}")
                    .AppendLine($"--Health: {hero.Health}")
                    .AppendLine($"--Armour: {hero.Armour}");

                string weaponInfo = hero.Weapon == null ? "Unarmed" : $"--Weapon: {hero.Weapon.Name}";
                sb.AppendLine(weaponInfo);
            }

            return sb.ToString().Trim();
        }

        private Type GetTypeByName(string type)
        {
            Assembly assembly = typeof(StartUp).Assembly;

            return assembly.GetTypes().FirstOrDefault(t => t.Name == type);
        }
    }
}
