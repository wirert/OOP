namespace MilitaryElite.Models
{
    using System;

    using Interfaces;

    public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
    {     
        public SpecialisedSoldier(int id, string firstName, string lastName, decimal salary, string corps) 
            : base(id, firstName, lastName, salary)
        {
            Corps = corps;
        }

        public string Corps { get; private set; }
        
        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + $"Corps: {Corps}";
        }
    }
}
