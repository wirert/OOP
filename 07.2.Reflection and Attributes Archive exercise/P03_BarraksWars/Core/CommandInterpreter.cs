namespace _03BarracksFactory.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Attributes;
    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private IRepository repository;
        private IUnitFactory unitFactory;

        public CommandInterpreter(IRepository repository, IUnitFactory unitFactory)
        {
            this.repository = repository;
            this.unitFactory = unitFactory;
        }

        public IExecutable InterpretCommand(string[] data, string commandName)
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            Type type = assembly.GetTypes()
                .FirstOrDefault(t => t.Name.ToLower().StartsWith(commandName.ToLower()));

            if (type == null)
            {
                throw new InvalidOperationException("Invalid Command!");
            }

            IExecutable commandInstance = (IExecutable)Activator.CreateInstance(type, new object[] { data });

            InjectFields(commandInstance);

            return commandInstance;
        }

        private void InjectFields(IExecutable commandInstance)
        {            
            var fields = commandInstance.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.GetCustomAttributes()
                .Any(ca => ca.GetType() == typeof(InjectAttribute)))
                .ToArray();

            var thisFields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach ( var field in fields)
            {
                field.SetValue(commandInstance, thisFields
                    .First(f => f.FieldType == field.FieldType).GetValue(this));
            }
        }
    }
}
