namespace PlanetWars.Models.MilitaryUnits
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            Cost = cost;
            enduranceLevel = 1;
        }

        public double Cost { get; private set; }

        public int EnduranceLevel => enduranceLevel;
        

        public void IncreaseEndurance()
        {
            enduranceLevel++;

            if (EnduranceLevel > 20)
            {
                enduranceLevel = 20;
                throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);
            }
        }
    }
}
