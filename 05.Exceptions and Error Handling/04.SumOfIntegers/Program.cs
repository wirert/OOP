using System;

namespace _04.SumOfIntegers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long sum = 0;

            string[] input = Console.ReadLine().Split();

            foreach (var item in input)
            {
                try
                {
                    sum += CheckInput(item);
                }
                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
                catch (OverflowException oe)
                {
                    Console.WriteLine(oe.Message);
                }
                finally
                {
                    Console.WriteLine($"Element '{item}' processed - current sum: {sum}");
                }
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");
        }

        public static int CheckInput(string input)
        {
            if (!long.TryParse(input, out long digit))
            {
                throw new FormatException($"The element '{input}' is in wrong format!");
            }

            if (digit < int.MinValue || digit > int.MaxValue)
            {
                throw new OverflowException($"The element '{input}' is out of range!");
            }

            return (int)digit;
        }
    }
}
