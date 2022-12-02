namespace Easter.Models.Dyes
{
    using Contracts;

    public class Dye : IDye
    {
        private const int Power_Decrease_Value_Using_Dye = 10;

        private int power;

        public Dye(int power)
        {
            Power = power;
        }

        public int Power
        {
            get => power;
            protected set
            {
                if (value <= 0)
                {
                    power = 0;
                }
                else
                {
                    power = value;
                }
            }
        }

        public bool IsFinished() => this.Power == 0;

        public void Use() => Power -= Power_Decrease_Value_Using_Dye;
    }
}
