using System;

namespace ExplicitInterfaces
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string name = parts[0];
                string country = parts[1];
                int age = int.Parse(parts[2]);

                IPerson citizen = new Citizen(name, country, age);
                citizen.GetName();
               IResident resident =  new Citizen(name, country, age);
                resident.GetName();
            }
        }
    }
}
