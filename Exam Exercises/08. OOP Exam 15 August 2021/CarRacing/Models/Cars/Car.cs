namespace CarRacing.Models.Cars
{
    using System;
    using Contracts;
    using Utilities.Messages;

    public abstract class Car : ICar
    {
        private const int VIN_Length = 17;

        private string make;
        private string model;
        private string vin;
        private int horsePower;
        private double fuelAvailable;
        private double fuelConsumptionPerRace;

        protected Car(string make, string model, string vin, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            VIN = vin;
            HorsePower = horsePower;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;
        }

        public string Make
        {
            get => make;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarMake);
                }

                make = value; 
            }
        }       

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarModel);
                }

                model = value;
            }
        }

        public string VIN
        {
            get => vin;
            private set
            {
                if (value.Length != VIN_Length)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarVIN);
                }

                vin = value;
            }
        }

        public int HorsePower
        {
            get => horsePower;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarHorsePower);
                }

                horsePower = value;
            }
        }

        public double FuelAvailable
        {
            get => fuelAvailable;
            private set
            {
                fuelAvailable = value < 0 ? 0 : value;
            }
        }

        public double FuelConsumptionPerRace
        {
            get => fuelConsumptionPerRace;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarFuelConsumption);
                }

                fuelConsumptionPerRace = value;
            }
        }

        public void Drive()
        {
            FuelAvailable -= FuelConsumptionPerRace;

            if (this.GetType().Name == "TunedCar")
            {
                HorsePower -= (int)Math.Round(HorsePower * 3.0 / 100);
            }
        }
    }
}
