namespace Easter.Models.Eggs
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public class Egg : IEgg
    {
        private const int EnergyRequired_Decrease_Value = 10;

        private string name;
        private int energyRequired;

        public Egg(string name, int energyRequired)
        {
            Name = name;
            EnergyRequired = energyRequired;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidEggName);
                }

                name = value;
            }
        }

        public int EnergyRequired
        {
            get => energyRequired;
            protected set
            {
                if (value <= 0)
                {
                    energyRequired = 0;
                }
                else
                {
                    energyRequired = value;
                }
            }
        }

        public void GetColored() => EnergyRequired -= EnergyRequired_Decrease_Value;

        public bool IsDone() => EnergyRequired == 0;
    }
}
