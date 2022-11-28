namespace Formula1.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models;
    using Models.Cars;
    using Repositories;
    using Utilities;

    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var pilot = pilotRepository.FindByName(pilotName);

            if (pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            var car = carRepository.FindByName(carModel);

            if (car == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var race = raceRepository.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            var pilot = pilotRepository.FindByName(pilotFullName);
            if (pilot == null
                || !pilot.CanRace
                || race.Pilots.Any(p => p.FullName == pilotFullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            FormulaOneCar car = null;

            if (carRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type == "Ferrari")
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == "Williams")
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            carRepository.Add(car);

            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreatePilot(string fullName)
        {
            if (pilotRepository.FindByName(fullName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            pilotRepository.Add(new Pilot(fullName));

            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (raceRepository.FindByName(raceName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            raceRepository.Add(new Race(raceName, numberOfLaps));

            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string StartRace(string raceName)
        {
            var race = raceRepository.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if (race.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var winners = race.Pilots
                 .OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps))
                 .Take(3).ToArray();
            race.TookPlace = true;
            winners.First().WinRace();

            return $"Pilot {winners[0].FullName} wins the {raceName} race." + Environment.NewLine
                + $"Pilot {winners[1].FullName} is second in the {raceName} race." + Environment.NewLine
                + $"Pilot {winners[2].FullName} is third in the {raceName} race.";
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var race in raceRepository.Models.Where(r => r.TookPlace))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().Trim();
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pilot in pilotRepository.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
