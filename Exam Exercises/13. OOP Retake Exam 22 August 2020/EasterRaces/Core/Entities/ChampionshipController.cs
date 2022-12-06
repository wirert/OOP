namespace EasterRaces.Core.Entities
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Cars.Contracts;
    using Models.Cars.Entities;
    using Models.Drivers.Contracts;
    using Models.Drivers.Entities;
    using Models.Races.Contracts;
    using Models.Races.Entities;
    using Repositories.Contracts;
    using Repositories.Entities;
    using Utilities.Messages;

    public class ChampionshipController : IChampionshipController
    {
        private const int MinDriversForRace = 3;

        private IRepository<IDriver> driverRepository;
        private IRepository<ICar> carRepository;
        private IRepository<IRace> raceRepository;

        public ChampionshipController()
        {
            driverRepository = new DriverRepository();
            carRepository = new CarRepository();
            raceRepository = new RaceRepository();
        }

        public string CreateDriver(string driverName)
        {
            if (driverRepository.GetByName(driverName) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }

            driverRepository.Add(new Driver(driverName));

            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (carRepository.GetByName(model) != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }

            ICar car = null;

            if (type == "Muscle")
            {
                car = new MuscleCar(model, horsePower);
            }
            else if (type == "Sports")
            {
                car = new SportsCar(model, horsePower);
            }
            else
            {
                throw new ArgumentException($"Could not find {type}");
            }

            carRepository.Add(car);

            return string.Format(OutputMessages.CarCreated, car.GetType().Name, model);
        }

        public string AddCarToDriver(string driverName, string carModel)
        {
            var driver = driverRepository.GetByName(driverName);
            var car = carRepository.GetByName(carModel);

            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            if (car == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }

            driver.AddCar(car);

            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            var race = raceRepository.GetByName(raceName);
            var driver = driverRepository.GetByName(driverName);

            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (driver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            race.AddDriver(driver);

            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }


        public string CreateRace(string name, int laps)
        {
            var race = raceRepository.GetByName(name);

            if (race != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            raceRepository.Add(new Race(name, laps));

            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            var race = raceRepository.GetByName(raceName);

            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (race.Drivers.Count < MinDriversForRace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, MinDriversForRace));
            }

            StringBuilder sb = new StringBuilder();

            var winners = race.Drivers.OrderByDescending(d => d.Car.CalculateRacePoints(race.Laps)).Take(3);

            winners.First().WinRace();

            sb.AppendLine(string.Format(OutputMessages.DriverFirstPosition, winners.First().Name, race.Name));
            sb.AppendLine(string.Format(OutputMessages.DriverSecondPosition, winners.Skip(1).First().Name, race.Name));
            sb.AppendLine(string.Format(OutputMessages.DriverThirdPosition, winners.Last().Name, race.Name));

            raceRepository.Remove(race);

            return sb.ToString().Trim();
        }
    }
}
