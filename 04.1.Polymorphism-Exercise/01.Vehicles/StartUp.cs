using System;
using System.Collections.Generic;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, Vehicle> vehicles = new Dictionary<string, Vehicle>();

            for (int i = 0; i < 3; i++)
            {
                ReadVehicle(vehicles);
            }
            
            int numCmd = int.Parse(Console.ReadLine());

            for (int i = 0; i < numCmd; i++)
            {
                ReadCommandForAVehicle(vehicles);
            }

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle.Value);
            }
        }

        private static void ReadVehicle(Dictionary<string, Vehicle> vehicles)
        {
            string[] vehicleInfo = Console.ReadLine().Split();
            string type = vehicleInfo[0];
            double vehicleFuel = double.Parse(vehicleInfo[1]);
            double vehicleConsumption = double.Parse(vehicleInfo[2]);
            double vehicleTankCapacity = double.Parse(vehicleInfo[3]);

            Vehicle vehicle = null;
            string key = null;

            switch (type)
            {
                
                case "Car":
                    vehicle = new Car(vehicleFuel, vehicleConsumption, vehicleTankCapacity);
                    key = "Car";
                    break;
                case "Truck":
                    vehicle = new Truck(vehicleFuel, vehicleConsumption, vehicleTankCapacity);
                    key = "Truck";
                    break;
                case "Bus":
                    vehicle = new Bus(vehicleFuel, vehicleConsumption, vehicleTankCapacity);
                    key = "Bus";
                    break;
            }

            vehicles.Add(key, vehicle);
        }

        private static void ReadCommandForAVehicle(Dictionary<string, Vehicle> vehicles)
        {
            string[] cmd = Console.ReadLine().Split();
            string type = cmd[1];
            string action = cmd[0];

            Vehicle vehicle = vehicles[type];

            if (action == "Drive")
            {
                double distance = double.Parse(cmd[2]);
                vehicle.Drive(distance);
            }
            else if (action == "Refuel")
            {
                double fuel = double.Parse(cmd[2]);
                vehicle.Refuel(fuel);
            }
            else if (action == "DriveEmpty")
            {
                double distance = double.Parse(cmd[2]);
                ((Bus)vehicle).Drive(distance, "without passengers");
            }
        }
    }
}
