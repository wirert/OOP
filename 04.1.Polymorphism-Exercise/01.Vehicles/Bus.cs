using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    internal class Bus : Vehicle
    {
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override void Drive(double distance)
        {
            FuelConsumption += 1.4;
            base.Drive(distance);
            FuelConsumption -= 1.4;
        }

        public void Drive(double distance, string people)
        {            
            base.Drive(distance);            
        }

        public override string ToString() => $"Bus: {FuelQuantity:f2}";
    }
}
