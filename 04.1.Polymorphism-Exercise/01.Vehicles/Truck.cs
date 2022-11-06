namespace Vehicles
{
    internal class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override double FuelConsumption => base.FuelConsumption + 1.6;        

        public override string ToString() => $"Truck: {FuelQuantity:f2}";
    }
}
