namespace CarRacing.Models.Racers
{
    using System;
    using Cars.Contracts;
    using Contracts;
    using Utilities.Messages;

    public abstract class Racer : IRacer
    {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }

        public string Username
        {
            get => username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                }

                username = value;
            }
        }

        public string RacingBehavior
        {
            get => racingBehavior;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                }

                racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => drivingExperience;
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);
                }

                drivingExperience = value;
            }
        }

        public ICar Car
        {
            get => car;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
                }

                car = value;
            }
        }

        public bool IsAvailable() => Car.FuelAvailable >= Car.FuelConsumptionPerRace;

        public void Race()
        {
            this.Car.Drive();

            if (this.GetType().Name == "ProfessionalRacer")
            {
                DrivingExperience += 10;
            }
            else if (this.GetType().Name == "StreetRacer")
            {
                DrivingExperience += 5;
            }
        }
    }
}
