using System;
using System.Linq;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine().Split();
            double carFuel = double.Parse(carInfo[1]);
            double carConsumption = double.Parse(carInfo[2]);

            Car car = new Car(carFuel, carConsumption);

            string[] truckInfo = Console.ReadLine().Split();
            double truckFuel = double.Parse(truckInfo[1]);
            double truckConsumption = double.Parse(truckInfo[2]);

            Truck truck = new Truck(truckFuel, truckConsumption);

            int numCmd = int.Parse(Console.ReadLine());

            for (int i = 0; i < numCmd; i++)
            {
                string[] cmd = Console.ReadLine().Split();

                if (cmd[0] == "Drive")
                {
                    double distance = double.Parse(cmd[2]);
                    if (cmd[1] == "Car")
                    {
                        car.Drive(distance);
                    }
                    else
                    {
                        truck.Drive(distance);
                    }
                }
                else
                {
                    double fuel = double.Parse(cmd[2]);
                    if (cmd[1] == "Car")
                    {
                        car.Refuel(fuel);
                    }
                    else
                    {
                        truck.Refuel(fuel);
                    }
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
        }
    }
}
