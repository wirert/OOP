using System;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                string animal = Console.ReadLine();

                if (animal == "Beast!")
                {
                    break;
                }

                string[] info = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (!int.TryParse(info[1], out int age))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                try
                {
                    var newAnimal = CreateAnimal(animal, info, age);

                    Console.WriteLine(animal);
                    Console.WriteLine(newAnimal);
                    Console.WriteLine(newAnimal.ProduceSound());
                }
                catch (InvalidOperationException exeption)
                {
                    Console.WriteLine(exeption.Message);
                }
            }
        }

        public static Animal CreateAnimal(string animal, string[] info, int age)
        {
            string name = info[0];

            switch (animal)
            {
                case "Dog": return new Dog(name, age, info[2]);
                case "Frog": return new Frog(name, age, info[2]);
                case "Cat": return new Cat(name, age, info[2]);
                case "Kitten": return new Kitten(name, age);
                case "Tomcat": return new Tomcat(name, age);
                default: return null;
            }
        }
    }
}
