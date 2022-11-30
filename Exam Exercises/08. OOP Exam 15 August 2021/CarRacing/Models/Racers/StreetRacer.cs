namespace CarRacing.Models.Racers
{
    using Cars.Contracts;

    public class StreetRacer : Racer
    {
        private const int Driving_Experience = 10;
        private const string Racing_Behavior = "aggressive";

        public StreetRacer(string username, ICar car)
            : base(username, Racing_Behavior, Driving_Experience, car)
        {
        }
    }
}
