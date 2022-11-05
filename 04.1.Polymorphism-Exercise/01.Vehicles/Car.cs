namespace Vehicles
{
    public class Car : Vehicle
    {


        public Car(double fuelQuantity, double fuelConsumption) 
            : base(fuelQuantity, fuelConsumption)
        {  }

        public override double FuelConsumption => base.FuelConsumption + 0.9;

        public override string ToString() => $"Car: {FuelQuantity:f2}";        
    }
}
