using System;
using System.Collections.Generic;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int numPersons = int.Parse(Console.ReadLine());
            var persons = new List<Person>();

            for (int i = 0; i < numPersons; i++)
            {
                string[] personInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    var person = new Person(personInfo[0], personInfo[1], int.Parse(personInfo[2]), decimal.Parse(personInfo[3]));
                    persons.Add(person);
                }
                catch (ArgumentException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }

            //decimal increaseSalaryPercent = decimal.Parse(Console.ReadLine());

            //foreach (var person in persons)
            //{
            //    person.IncreaseSalary(increaseSalaryPercent);

            //    Console.WriteLine(person);
            //}

            Team team = new Team("SoftUni");

            foreach (Person person in persons)
            {
                team.AddPlayer(person);
            }

            Console.WriteLine($"First team has {team.FirstTeam.Count} players.");
            Console.WriteLine($"Reserve team has {team.ReserveTeam.Count} players.");
        }
    }
}
