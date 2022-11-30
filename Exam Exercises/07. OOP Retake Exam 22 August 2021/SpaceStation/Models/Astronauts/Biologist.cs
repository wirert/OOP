namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const double BIOLOGIST_OXYGEN = 70;

        public Biologist(string name) : base(name, BIOLOGIST_OXYGEN)
        {
        }

        public override void Breath() => this.Oxygen -= 5;        
    }
}
