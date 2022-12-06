namespace EasterRaces.Models.Drivers.Entities
{
    using System;

    using Cars.Contracts;
    using Contracts;
    using Utilities.Messages;

    public class Driver : IDriver
    {
        private const int MinLengthOfName = 5;

        private string name;


        public Driver(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < MinLengthOfName)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, MinLengthOfName));
                }

                name = value;
            }
        }

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate => Car != null;

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(Car), ExceptionMessages.CarInvalid);
            }

            Car = car;
        }

        public void WinRace() => NumberOfWins++;
    }
}
