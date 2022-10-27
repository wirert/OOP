using System;

namespace Person
{
    public class Child : Person
    {
        public Child(string name, int age) : base(name, age) { }

        public override int Age
        {
            get => base.Age;
            set
            {
                if (value <= 15)
                    base.Age = value;
            }
            // set => base.Age = base.Age <= 15 ? value : throw new InvalidOperationException("Invalid Age");
        }
    }
}
