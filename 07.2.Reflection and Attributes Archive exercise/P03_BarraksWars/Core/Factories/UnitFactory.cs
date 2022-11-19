namespace _03BarracksFactory.Core.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Metadata.Ecma335;
    using Contracts;

    public class UnitFactory : IUnitFactory
    {
        public IUnit CreateUnit(string unitType)
        {            
            Assembly assembly = Assembly.GetEntryAssembly();
            
            var typeUnit = assembly.GetTypes().FirstOrDefault(t => t.Name.ToLower() == unitType.ToLower());

            if (typeUnit == null)
            {
                throw new InvalidOperationException("Invalid unit name.");
            }

            var unit = (IUnit)Activator.CreateInstance(typeUnit, true);

            return unit; 
        }
    }
}
