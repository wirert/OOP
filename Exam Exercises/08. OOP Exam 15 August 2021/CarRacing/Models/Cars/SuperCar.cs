namespace CarRacing.Models.Cars
{
    public class SuperCar : Car
    {
        private const double Fuel_Available = 80;
        private const double Fuel_Consumption_Per_Race = 10;

        public SuperCar(string make, string model, string vin, int horsePower) 
            : base(make, model, vin, horsePower, Fuel_Available, Fuel_Consumption_Per_Race)
        {
        }
    }
}
