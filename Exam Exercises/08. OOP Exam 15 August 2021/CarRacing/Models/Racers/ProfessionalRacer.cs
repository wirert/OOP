namespace CarRacing.Models.Racers
{
    using Cars.Contracts;

    public class ProfessionalRacer : Racer
    {
        private const int Driving_Experience = 30;
        private const string Racing_Behavior = "strict";

        public ProfessionalRacer(string username, ICar car)
            : base(username, Racing_Behavior, Driving_Experience, car)
        {
        }
    }
}
