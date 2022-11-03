using System;

namespace ExplicitInterfaces
{
    public class Citizen : IResident, IPerson
    {
        public Citizen(string name, string country, int age)
        {
            Name = name;
            Country = country;
            Age = age;
        }

        public string Name { get; private set; }
        public string Country { get; set; }
        public int Age { get; set; }

        void IPerson.GetName()
        {
            Console.WriteLine(Name);
        }

        void IResident.GetName()
        {
            Console.WriteLine($"Mr/Ms/Mrs {Name}");
        }
    }
}
