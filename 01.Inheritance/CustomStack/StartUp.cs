using System;

namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            StackOfStrings stack = new StackOfStrings();

            stack.Push("1");
            stack.Push("2");
            stack.Push("3");
            stack.Push("4");
            stack.Push("5");

            stack.AddRange(new string[] { "6", "7", "8", "9" });

            Console.WriteLine(string.Join(" ", stack));
        }
    }
}
