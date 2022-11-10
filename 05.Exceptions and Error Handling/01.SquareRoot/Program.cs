using System;

namespace SquareRoot
{
    public class Program
    {
        static void Main(string[] args)
        {
			try
			{
				SquraRoodOfNumber();
			}
			catch (ArithmeticException ae)
			{
				Console.WriteLine(ae.Message);
			}
			finally
			{
				Console.WriteLine("Goodbye.");
			}	
        }

		private static void SquraRoodOfNumber()
		{
			double number = double.Parse(Console.ReadLine());

			if (number < 0)
			{
				throw new ArithmeticException("Invalid number.");
			}

			Console.WriteLine(Math.Sqrt(number));
		}
    }
}
