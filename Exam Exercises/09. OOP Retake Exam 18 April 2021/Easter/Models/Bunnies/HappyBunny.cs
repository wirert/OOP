namespace Easter.Models.Bunnies
{
    public class HappyBunny : Bunny
    {
        private const int Initial_Energy = 100;        

        public HappyBunny(string name) : base(name, Initial_Energy)
        {
        }
    }
}
