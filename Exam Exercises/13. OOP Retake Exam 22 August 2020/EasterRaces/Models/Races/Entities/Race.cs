namespace EasterRaces.Models.Races.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Drivers.Contracts;
    using Utilities.Messages;

    public class Race : IRace
    {
        private const int MinLengthOfName = 5;
        private const int MinNumberOfLaps = 1;

        private string name;
        private int laps;
        private readonly ICollection<IDriver> drivers;


        public Race(string name, int laps)
        {
            Name= name;
            Laps = laps;
            drivers = new HashSet<IDriver>();
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
        public int Laps
        {
            get => laps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, MinNumberOfLaps));
                }

                laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => drivers as IReadOnlyCollection<IDriver>;

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(Drivers), ExceptionMessages.DriverInvalid);
            }

            if (driver.CanParticipate == false)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }
            // it's not clear whether driver name is unique
            if (drivers.Any(d => d.Name == driver.Name))
            {
                throw new ArgumentNullException(nameof(Drivers), string.Format(ExceptionMessages.DriverAlreadyAdded, driver.Name, this.Name));
            }

            drivers.Add(driver);
        }
    }
}
