using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.PlayCatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            int exeptionCount = 0;

            while (exeptionCount < 3)
            {
                string[] cmd = Console.ReadLine().Split();

                string action = cmd[0];

                try
                {
                    switch (action)
                    {
                        case "Replace":
                            int index = ParseIndex(cmd[1], arr);
                            int element = ParseElement(cmd[2]);

                            arr[index] = element;
                            break;
                        case "Print":
                            int startIndex = ParseIndex(cmd[1], arr);
                            int endIndex = ParseIndex(cmd[2], arr);
                            List<int> ints = new List<int>();

                            for (int i = startIndex; i <= endIndex; i++)
                            {
                                ints.Add(arr[i]);
                            }

                            Console.WriteLine(string.Join(", ", ints));
                            break;
                        case "Show":
                            int showIndex = ParseIndex(cmd[1], arr);

                            Console.WriteLine(arr[showIndex]);
                            break;
                    }
                }
                catch (InvalidFormatExeption ife)
                {
                    Console.WriteLine(ife.Message);
                    exeptionCount++;
                }
                catch (IndexExeption ie)
                {
                    Console.WriteLine(ie.Message);
                    exeptionCount++;
                }
            }

            Console.WriteLine(string.Join(", ", arr));
        }

        private static int ParseIndex(string strIndex, int[] arr)
        {
            if (!int.TryParse(strIndex, out int index))
            {
                throw new InvalidFormatExeption();
            }
            if (index < 0 || index >= arr.Length)
            {
                throw new IndexExeption();
            }

            return index;
        }

        private static int ParseElement(string strElement)
        {
            if (!int.TryParse(strElement, out int intElement))
            {
                throw new InvalidFormatExeption();
            }

            return intElement;
        }
    }

    class IndexExeption : Exception
    {
        public IndexExeption() : base("The index does not exist!")
        { }
    }

    class InvalidFormatExeption : Exception
    {
        public InvalidFormatExeption() : base("The variable is not in the correct format!")
        { }
    }
}
