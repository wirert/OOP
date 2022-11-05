using System;

namespace Vehicles
{
    public abstract class Vehicle
    {   
        protected Vehicle(double fuelQuantity, double fuelConsumption)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }

        public double FuelQuantity { get; protected set; }
        public virtual double FuelConsumption { get; private set; }

        public virtual void Drive(double distance)
        {
            double neededFuel = FuelConsumption * distance;

            if (neededFuel > FuelQuantity)
            {
                Console.WriteLine($"{this.GetType().Name} needs refueling");
            }
            else
            {
                FuelQuantity -= neededFuel;
                Console.WriteLine($"{this.GetType().Name} travelled {distance} km");
            }
        }

        public virtual void Refuel(double fuel) => FuelQuantity += fuel;
    }
}
