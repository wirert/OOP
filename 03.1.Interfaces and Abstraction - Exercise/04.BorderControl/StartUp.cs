using System;
using System.Collections.Generic;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
           List<IIdentifiable> robotsAndCitizens = new List<IIdentifiable>();

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();

                if (tokens.Length == 2)
                {
                    IIdentifiable robot = new Robot(tokens[0], tokens[1]);
                    robotsAndCitizens.Add(robot);
                }
                else if (tokens.Length == 3)
                {
                    IIdentifiable citizen = new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2]);
                    robotsAndCitizens.Add(citizen);                    
                }
            }

            string lastDigits = Console.ReadLine();

            foreach (IIdentifiable unit in robotsAndCitizens)
            {                
                if (unit.Id.EndsWith(lastDigits))
                {
                    Console.WriteLine(unit.Id);
                }
            }
        }
    }
}
