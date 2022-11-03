using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionHierarchy
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] strings = Console.ReadLine().Split();

            List<IAddRemoveCollection> collections = new List<IAddRemoveCollection>
            {
                { new AddCollection() },
                { new AddRemoveCollection() },
                { new MyList() }
            };

            foreach (var collection in collections)
            {
                foreach (var text in strings)
                {
                    Console.Write($"{collection.Add(text)} ");
                }

                Console.WriteLine();
            }

            int numRemoves = int.Parse(Console.ReadLine());

            foreach (var collection in collections.Skip(1))
            {
                for (int i = 0; i < numRemoves; i++)
                {
                    Console.Write($"{collection.Remove()} ");
                }

                Console.WriteLine();
            }
        }
    }
}
