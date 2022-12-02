namespace Easter.Models.Bunnies
{
    public class SleepyBunny : Bunny
    {
        private const int Initial_Energy = 50;
        private const int Energy_Work_Additional_Decrease_Value = 5;

        public SleepyBunny(string name) : base(name, Initial_Energy)
        {
        }

        public override void Work()
        {
            base.Work();
            this.Energy -= Energy_Work_Additional_Decrease_Value;
        }
    }
}
