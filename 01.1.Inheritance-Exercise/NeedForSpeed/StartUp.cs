using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {


            FamilyCar car = new FamilyCar(500, 70.3);

            //Console.WriteLine(car.DefaultFuelConsumption);
            Console.WriteLine(car.FuelConsumption);

            car.Drive(20);
            Console.WriteLine(car.Fuel);

            SportCar sportCar = new SportCar(500, 50);
            sportCar.Drive(20);
            Console.WriteLine(sportCar.Fuel);
            Console.WriteLine(sportCar.FuelConsumption);

            RaceMotorcycle bike = new RaceMotorcycle(200, 25.5);

            //Console.WriteLine(bike.DefaultFuelConsumption);
            Console.WriteLine(bike.FuelConsumption);

            CrossMotorcycle cross = new CrossMotorcycle(150, 22.5);

           // Console.WriteLine(cross.DefaultFuelConsumption);
            Console.WriteLine(cross.FuelConsumption);
        }
    }
}
