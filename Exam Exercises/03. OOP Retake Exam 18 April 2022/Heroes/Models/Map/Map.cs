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
            List<IHero> knights = new List<IHero>();
            List<IHero> barbarians = new List<IHero>();

            foreach (var hero in players)
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

            while (barbarians.Count != deadBarbarians && knights.Count != deadKnights)
            {
                deadBarbarians = AttackAllOpponents(knights, barbarians, deadBarbarians);
                
                deadKnights = AttackAllOpponents(barbarians, knights, deadKnights);                
            }

            string result = barbarians.Count == deadBarbarians 
                ? string.Format(OutputMessages.BattleResult, "knights", deadKnights)
                : string.Format(OutputMessages.BattleResult, "barbarians", deadBarbarians);

            return result;
        }

        private int AttackAllOpponents(List<IHero> attackers, List<IHero> defenders, int deadDefenders)
        {
            foreach (var attacker in attackers.Where(a => a.IsAlive))
            {
                foreach (var defender in defenders.Where(d => d.IsAlive))
                {
                    defender.TakeDamage(attacker.Weapon.DoDamage());

                    if (!defender.IsAlive)
                    {
                        deadDefenders++;
                    }
                }
            }

            return deadDefenders;
        }        
    }
}
