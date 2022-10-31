using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.FootballTeamGenerator
{
    public class Team
    {
        private List<Player> players;
        private string name;
        

        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                name = value;
            }
        }

        public int Rating
        {
            get 
            {
                int rating = 0;
                foreach (Player p in players)
                {
                    rating += p.SkillLevel;
                }

                return rating; 
            }
        }

        public void AddPlayer(Player player) => players.Add(player);

        public void RemovePlayer(string playerName)
        {
            Player player = players.FirstOrDefault(p => p.Name == playerName);

            if (player == null)
            {
                throw new Exception($"Player {playerName} is not in {this.Name} team.");
            }

            players.Remove(player);
        } 
    }
}
