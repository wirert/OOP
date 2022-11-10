using System;
using System.Collections.Generic;

namespace EnterNumbers
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            List<int> nums = new List<int>();

             int startNum = 1;
            const int EndNum = 100;
            const int NumCount = 10;



            while (nums.Count < NumCount)
            {
                try
                {
                    if (nums.Count > 0)
                    {
                        startNum = nums[nums.Count - 1];
                    }
                    nums.Add(ReadNumber(startNum, EndNum));
                }
                catch (ArgumentException ae)
                {

                    Console.WriteLine(ae.Message);
                }
            }

            Console.WriteLine(string.Join(", ", nums));
        }

        private static int ReadNumber(int startNum, int endNum)
        {
            if (!int.TryParse(Console.ReadLine(), out int num))
            {
                throw new ArgumentException("Invalid Number!");
            }
            else
            {
                if (num <= startNum || num >= endNum)
                {
                    throw new ArgumentException($"Your number is not in range {startNum} - 100!");
                }
            }

            return num;
        }
    }
}
