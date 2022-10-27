using System;

namespace Zoo
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Bear bear = new Bear("Mecho");

            Console.WriteLine(bear.Name);
        }
    }
}