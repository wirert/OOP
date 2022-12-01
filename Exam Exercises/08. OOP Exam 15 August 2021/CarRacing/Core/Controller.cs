namespace CarRacing.Core
{
    using System;
    using System.Reflection;
    using System.Text;
    using System.Linq;
    using CarRacing.Models.Cars;
    using CarRacing.Models.Cars.Contracts;
    using CarRacing.Models.Maps;
    using CarRacing.Models.Maps.Contracts;
    using CarRacing.Models.Racers;
    using CarRacing.Models.Racers.Contracts;
    using CarRacing.Repositories;
    using CarRacing.Repositories.Contracts;
    using CarRacing.Utilities.Messages;
    using Contracts;

    public class Controller : IController
    {
        private IRepository<ICar> cars;
        private IRepository<IRacer> racers;
        private IMap map;

        public Controller()
        {
            cars = new CarRepository();
            racers = new RacerRepository();
            map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car = null;

            if (type == "SuperCar")
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == "TunedCar")
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }

            cars.Add(car);

            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            IRacer racer = null;

            ICar car = cars.FindBy(carVIN);

            if (car == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }

            if (type == "ProfessionalRacer")
            {
                racer = new ProfessionalRacer(username, car);
            }
            else if (type == "StreetRacer")
            {
                racer = new StreetRacer(username, car);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }

            racers.Add(racer);

            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = racers.FindBy(racerOneUsername);
            IRacer racerTwo = racers.FindBy(racerTwoUsername);

            if (racerOne == null || racerTwo == null)
            {
                string notFoundRacerName = racerOne == null ? racerOneUsername : racerTwoUsername;

                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, notFoundRacerName));
            }

            return map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var racer in racers.Models
                .OrderByDescending(r => r.DrivingExperience)
                .ThenBy(r => r.Username))
            {
                sb.AppendLine($"{racer.GetType().Name}: {racer.Username}")
                    .AppendLine($"--Driving behavior: {racer.RacingBehavior}")
                    .AppendLine($"--Driving experience: {racer.DrivingExperience}")
                    .AppendLine($"--Car: {racer.Car.Make} {racer.Car.Model} ({racer.Car.VIN})");
            }

            return sb.ToString().Trim();
        }
    }
}
