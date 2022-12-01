namespace CarRacing.Models.Maps
{
    using Contracts;
    using Racers.Contracts;
    using Utilities.Messages;

    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }
            else if (racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }
            else if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }
            else
            {
                racerOne.Race();
                racerTwo.Race();

                double racerOneChance = GetChanceOfWinning(racerOne);
                double racerTwoChance = GetChanceOfWinning(racerTwo);

                IRacer winner = racerOneChance > racerTwoChance ? racerOne : racerTwo;

                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner.Username);
            }
        }

        private double GetChanceOfWinning(IRacer racer)
        {
            double racingBehaviorMultiplyer = racer.RacingBehavior == "strict" ? 1.2 : 1.1;

            return racer.Car.HorsePower * racer.DrivingExperience * racingBehaviorMultiplyer;
        }
    }
}
