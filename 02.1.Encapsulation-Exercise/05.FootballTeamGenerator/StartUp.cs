using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();

            string command = null;

            while ((command = Console.ReadLine()) != "END" )
            {
                string[] tokens = command.Split(";");
                string teamName = tokens[1];
                try
                {
                    switch (tokens[0])
                    {
                        case "Team":
                            teams.Add(new Team(teamName));
                            break;
                        case "Rating":
                            Team team = teams.FirstOrDefault(t => t.Name == teamName);

                            if (team == null)
                            {
                                Console.WriteLine($"Team {teamName} does not exist.");
                            }
                            else
                            {
                                Console.WriteLine($"{teamName} - {team.Rating}");
                            }
                            break;
                        case "Add":
                            Team teamToAddIn = teams.FirstOrDefault(t => t.Name == teamName);

                            if (teamToAddIn == null)
                            {
                                Console.WriteLine($"Team {teamName} does not exist.");
                            }
                            else
                            {
                                teamToAddIn.AddPlayer(new Player(tokens[2], int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]), int.Parse(tokens[6]), int.Parse(tokens[7])));
                            }
                            break;
                        case "Remove":
                            Team teamToRemoveFrom = teams.FirstOrDefault(t => t.Name == teamName);

                            if (teamToRemoveFrom == null)
                            {
                                Console.WriteLine($"Team {teamName} does not exist.");
                            }
                            else
                            {
                                teamToRemoveFrom.RemovePlayer(tokens[2]);
                            }
                                break;                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
