using System;

namespace Animals
{
    public class Animal
    {
        private string name;
        private int age;
        private string gender;

        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException("Invalid input!");
                }

                name = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value >= 0)
                {
                    age = value;
                }
                else
                {
                    throw new InvalidOperationException("Invalid input!");
                }
            }

        }
        public string Gender
        {
            get => gender;
            set
            {
                if (value == "Male" || value == "Female")
                {
                    gender = value;
                }
                else
                {
                    throw new InvalidOperationException("Invalid input!");
                }
            }
        }

        public virtual string ProduceSound() => "Bau";

        public override string ToString() => $"{Name} {Age} {Gender}";
    }
}
