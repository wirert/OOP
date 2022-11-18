namespace CommandPattern.Utilities
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Core.Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string cmdArgs)
        {
            string commandName = cmdArgs.Split()[0];
            string[] args = cmdArgs.Split().Skip(1).ToArray();

            Assembly assembly = Assembly.GetEntryAssembly();
            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == $"{commandName}Command");

            if (type == null)
            {
                throw new InvalidOperationException("Invalid Command type!");
            }

            var instance = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("Execute");

            if (method == null)
            {
                throw new InvalidOperationException($"ICommand pattern isn't implemented in {commandName}Command class.");
            }

            return method.Invoke(instance, new object[] { args }) as string;
        }
    }
}
