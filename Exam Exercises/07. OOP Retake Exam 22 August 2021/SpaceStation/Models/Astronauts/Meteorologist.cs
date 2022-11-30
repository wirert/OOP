namespace SpaceStation.Models.Astronauts
{
    public class Meteorologist : Astronaut
    {
        private const double METEOROLOGIST_OXYGEN = 90;

        public Meteorologist(string name) : base(name, METEOROLOGIST_OXYGEN)
        {
        }
    }
}
