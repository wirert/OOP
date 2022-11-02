using System;

namespace BirthdayCelebrations
{
    public class Citizen : IIdentifiable, IBirthDateble
    {
        private string name;
        private int age;
        private string id;
        private DateTime birthdate;
        
        public Citizen(string name, int age, string id, DateTime birthDate)
        {
            this.name = name;
            this.age = age;   
            this.id = id;
            this.birthdate = birthDate;
        }

        public string Id => id;
        public string Name => name;
        public int Age => age;
        public DateTime Birthdate => birthdate;
    }
}
