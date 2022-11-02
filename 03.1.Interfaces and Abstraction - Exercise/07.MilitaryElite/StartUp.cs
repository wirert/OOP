using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MilitaryElite
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string command;

            List<Soldier> soldierList = new List<Soldier>();

            List<Private> privates = new List<Private>();

            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string typeSoldier = tokens[0];
                int id = int.Parse(tokens[1]);
                string firstName = tokens[2];
                string lastName = tokens[3];

                switch (typeSoldier)
                {
                    case "Private":
                        Private rednik = new Private(id, firstName, lastName, decimal.Parse(tokens[4]));
                        privates.Add(rednik);
                        soldierList.Add(rednik);
                        break;
                    case "LieutenantGeneral":
                        AddLieutenantGeneral(soldierList, privates, tokens);
                        break;
                    case "Engineer":
                        try
                        {
                            AddEngineer(tokens, soldierList);
                        }
                        catch (ArgumentException) { }
                        
                        break;
                    case "Commando":
                        try
                        {
                            AddCommando(tokens, soldierList);
                        }
                        catch (ArgumentException) { }
                        
                        break;
                    case "Spy":
                        soldierList.Add(new Spy(id, firstName, lastName, int.Parse(tokens[4])));
                        break;                    
                }
            }

            foreach (var soldier in soldierList)
            {
                Console.WriteLine(soldier);
            }
        }

        private static void AddLieutenantGeneral(List<Soldier> soldierList, List<Private> privates, string[] tokens)
        {
            int id = int.Parse(tokens[1]);
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            List<Private> privatesInCommand = new List<Private>();

            for (int i = 5; i < tokens.Length; i++)
            {
                privatesInCommand.Add(privates.FirstOrDefault(p => p.Id == int.Parse(tokens[i])));
            }

            soldierList.Add(new LieutenantGeneral(id, firstName, lastName, salary, privatesInCommand));
        }

        public static void AddEngineer(string[] tokens, List<Soldier> soldierList)
        {
            int id = int.Parse(tokens[1]);
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            string corps = tokens[5];
            Dictionary<string, int> repairs = new Dictionary<string, int>();

            for (int i = 6; i < tokens.Length - 1; i += 2)
            {
                repairs.Add(tokens[i], int.Parse(tokens[i + 1]));
            }

            Engineer engineer = new Engineer(id, firstName, lastName, salary, corps, repairs);
            soldierList.Add(engineer);
        }

        public static void AddCommando(string[] tokens, List<Soldier> soldierList)
        {
            int id = int.Parse(tokens[1]);
            string firstName = tokens[2];
            string lastName = tokens[3];
            decimal salary = decimal.Parse(tokens[4]);
            string corps = tokens[5];
            List<Mission> missions = new List<Mission>();

            for (int i = 6; i < tokens.Length - 1; i += 2)
            {
                try
                {
                    missions.Add(new Mission(tokens[i], tokens[i + 1]));
                }
                catch (ArgumentException) { } 
            }

            soldierList.Add(new Commando(id, firstName,lastName,salary,corps, missions));
        }
    }
}
