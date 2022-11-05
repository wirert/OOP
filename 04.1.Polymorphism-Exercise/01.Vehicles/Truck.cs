﻿namespace Vehicles
{
    internal class Truck : Vehicle
    {
        public Truck(double fuelQuantity, double fuelConsumption) 
            : base(fuelQuantity, fuelConsumption)
        { }

        public override double FuelConsumption => base.FuelConsumption + 1.6;

        public override void Refuel(double fuel)
        {
           FuelQuantity += 0.95 * fuel;
        }

        public override string ToString() => $"Truck: {FuelQuantity:f2}";       
    }
}
