namespace CommandPattern.Core
{
    using System;

    using Contracts;
    using IO;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        private Engine()
        {
            reader = new ConsoleReader();
            writer = new ConsoleWriter();
        }

        public Engine(ICommandInterpreter commandInterpreter)
            : this()
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            string input;

            while ((input = reader.ReadLine()) != "End")
            {
                try
                {
                    writer.WriteLine(commandInterpreter.Read(input));
                }
                catch (InvalidOperationException ioe)
                {
                    writer.WriteLine(ioe.Message);
                }                
            }            
        }
    }
}
