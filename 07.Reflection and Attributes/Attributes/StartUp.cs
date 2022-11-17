using System;

namespace Attributes
{

    [Author("Wirert")]
    public class StartUp
    {

        [Author("Krasi")]
        static void Main(string[] args)
        {
            var tracker = new Tracker();

            tracker.PrintMethodsByAuthor();
        }
    }
}
