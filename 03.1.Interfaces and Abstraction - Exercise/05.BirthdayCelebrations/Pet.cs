using System;

namespace BirthdayCelebrations
{
    public class Pet : IBirthDateble
    {
        private string name;
        private DateTime birthdate;

        public Pet(string name, DateTime birthdate)
        {
            this.name = name;
            this.birthdate = birthdate;
        }

        public string Name => name;
        public DateTime Birthdate => birthdate;
    }
}
