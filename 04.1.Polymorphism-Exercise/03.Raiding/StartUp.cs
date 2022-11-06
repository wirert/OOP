using Raiding.Models;
using System;
using System.Collections.Generic;

namespace Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int heroesNeeded = int.Parse(Console.ReadLine());
            int numHeroes = 0;
            HashSet<BaseHero> heroes = new HashSet<BaseHero>();

            while (heroes.Count < heroesNeeded)
            {
                ReadHero(heroes);
            }

            int bossPower = int.Parse(Console.ReadLine());
            int damageDone = 0;

            foreach (var hero in heroes)
            {
                damageDone += hero.Power;
                Console.WriteLine(hero.CastAbility());
            }

            Console.WriteLine(bossPower > damageDone ? "Defeat..." : "Victory!");
        }

        static void ReadHero(HashSet<BaseHero> heroes)
        {
            string heroName = Console.ReadLine();
            string heroType = Console.ReadLine();

            BaseHero hero = null;

            switch (heroType)
            {
                case "Druid":
                    hero = new Druid(heroName);
                    break;
                case "Paladin":
                    hero = new Paladin(heroName);
                    break;
                case "Rogue":
                    hero = new Rogue(heroName);
                    break;
                case "Warrior":
                    hero = new Warrior(heroName);
                    break;
                default:
                    Console.WriteLine("Invalid hero!");
                    return;
            }

            heroes.Add(hero);
        }
    }
}
