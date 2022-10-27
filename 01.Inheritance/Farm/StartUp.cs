using System;

namespace Farm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog();

            dog.Eat();
            dog.Bark();

            Puppy puppy = new Puppy();

            puppy.Weep();
            puppy.Eat();
            puppy.Bark();

            Cat cat = new Cat();

            cat.Eat();
            cat.Meow();
        }
    }
}
