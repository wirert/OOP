namespace SpaceStation.Models.Astronauts
{
    public class Geodesist : Astronaut
    {
        private const double GEODESIST_OXYGEN = 50;

        public Geodesist(string name) : base(name, GEODESIST_OXYGEN)
        {
        }
    }
}
