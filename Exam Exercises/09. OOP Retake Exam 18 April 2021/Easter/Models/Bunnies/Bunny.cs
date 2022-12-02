namespace Easter.Models.Bunnies
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Dyes.Contracts;
    using Utilities.Messages;

    public abstract class Bunny : IBunny
    {
        private const int Energy_Work_Decrease_Value = 10;

        private string name;
        private int energy;        

        protected Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            Dyes = new HashSet<IDye>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }

                name = value;
            }
        }

        public int Energy
        {
            get => energy;
            protected set
            {
                if (value <= 0)
                {
                    energy = 0;
                }
                else
                {
                    energy = value;
                }
            }
        }

        public ICollection<IDye> Dyes { get; private set; }

        public void AddDye(IDye dye) => Dyes.Add(dye);

        public virtual void Work()
        {
            this.Energy -= Energy_Work_Decrease_Value;
        }
    }
}
