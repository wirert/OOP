using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BirthdayCelebrations
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBirthDateble> birthdays = new List<IBirthDateble>();

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string type = tokens[0];

                if (type == "Pet")
                {
                    DateTime birthday = DateTime.ParseExact(tokens[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    IBirthDateble pet = new Pet(tokens[1], birthday);
                    birthdays.Add(pet);
                }
                else if (type == "Citizen")
                {
                    DateTime birthday = DateTime.ParseExact(tokens[4], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    IBirthDateble citizen = new Citizen(tokens[1], int.Parse(tokens[2]), tokens[3], birthday);
                    birthdays.Add(citizen);
                }
            }

            int year = int.Parse(Console.ReadLine());

            foreach (var item in birthdays.Where(i => i.Birthdate.Year == year))
            {
                Console.WriteLine(item.Birthdate.ToString("dd/MM/yyyy"));
            }
        }
    }
}
