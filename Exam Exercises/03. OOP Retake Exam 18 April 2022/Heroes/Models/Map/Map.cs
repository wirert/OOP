namespace Heroes.Models.Map
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Heroes;
    using Utilities.Messages;

    public class Map : IMap
    {    
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights= new List<IHero>();
            List<IHero> barbarians= new List<IHero>();

            foreach (var hero in players.Where(h => h.IsAlive))
            {
                if (hero is Knight)
                {
                    knights.Add(hero);
                }
                else if (hero is Barbarian)
                {
                    barbarians.Add(hero);
                }
            }

            int deadKnights = 0;
            int deadBarbarians = 0;

            while (barbarians.Count != 0 && knights.Count != 0)
            {
                foreach (var knight in knights)
                {
                    for (int i = 0; i < barbarians.Count; i++)                    
                    {
                        barbarians[i].TakeDamage(knight.Weapon.DoDamage());

                        if (!barbarians[i].IsAlive)
                        {
                            deadBarbarians++;
                            barbarians.RemoveAt(i);

                            if (barbarians.Count == 0)
                            {
                                return string.Format(OutputMessages.BattleResult, "knights", deadKnights);
                            }
                        }
                    }
                }

                foreach (var barbarian in barbarians)
                {
                    for (int i = 0; i < knights.Count; i++)
                    {
                        knights[i].TakeDamage(barbarian.Weapon.DoDamage());

                        if (!knights[i].IsAlive)
                        {
                            deadKnights++;
                            knights.RemoveAt(i);

                            if (knights.Count == 0)
                            {
                                return string.Format(OutputMessages.BattleResult, "barbarians", deadBarbarians);
                            }
                        }
                    }
                }
            }

            return "The knights took 0 casualties but won the battle.";
        }
    }
}
