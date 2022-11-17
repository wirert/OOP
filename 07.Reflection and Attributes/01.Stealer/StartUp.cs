using System;

namespace Stealer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Spy spy = new Spy();

            string result = spy.StealFieldInfo("Stealer.Hacker", "username", "password");

            Console.WriteLine(result);

            string analyze = spy.AnalyzeAccessModifiers("Stealer.Hacker");

            Console.WriteLine(analyze);
        }
    }
}
