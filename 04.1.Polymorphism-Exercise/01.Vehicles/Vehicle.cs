using System;

namespace Vehicles
{
    public abstract class Vehicle
    {
        private double fuelQuantity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }

        public double FuelQuantity 
        { 
            get => fuelQuantity; 
            protected set
            {
                if (value <= TankCapacity)
                {
                    fuelQuantity = value;
                }               
            } 
        }
        public virtual double FuelConsumption { get; protected set; }
        public double TankCapacity { get; private set; }

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

        public virtual void Refuel(double fuel)
        {
            if (fuel <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
            }
            else if (fuel > TankCapacity)
            {
                Console.WriteLine($"Cannot fit {fuel} fuel in the tank");
            }
            else
            {
                FuelQuantity += fuel;

                if (this.GetType().Name == "Truck")
                {
                    FuelQuantity -= 0.05 * fuel;
                }
            }
        }
    }
}
