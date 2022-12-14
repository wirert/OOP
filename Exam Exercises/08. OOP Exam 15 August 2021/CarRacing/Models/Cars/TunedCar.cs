namespace CarRacing.Models.Cars
{
    using System;

    public class TunedCar : Car
    {
        private const double Fuel_Available = 65;
        private const double Fuel_Consumption_Per_Race = 7.5;

        public TunedCar(string make, string model, string vin, int horsePower)
            : base(make, model, vin, horsePower, Fuel_Available, Fuel_Consumption_Per_Race)
        {
        }

        public override void Drive()
        {
            base.Drive();
            HorsePower -= (int)Math.Round(HorsePower * 3.0 / 100);
        }
    }
}
