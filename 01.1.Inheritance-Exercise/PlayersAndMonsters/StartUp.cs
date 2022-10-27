using System;

namespace PlayersAndMonsters
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            BladeKnight knight = new BladeKnight("pesho", 20);

            Console.WriteLine(knight);
        }
    }
}