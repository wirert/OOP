namespace EasterRaces.Models.Cars.Entities
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public abstract class Car : ICar
    {
        private const int MinLengthOfModel = 4;

        private string model;
        private int horsePower;
        private int minHorsePower;
        private int maxHorsePower;

        public Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            Model = model;
            CubicCentimeters = cubicCentimeters;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
            HorsePower = horsePower;
            
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < MinLengthOfModel)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, MinLengthOfModel));
                }

                model = value;
            }
        }

        public int HorsePower
        {
            get => horsePower;
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, value));
                }

                horsePower = value;
            }
        }

        public double CubicCentimeters { get; private set; }

        public double CalculateRacePoints(int laps) => CubicCentimeters / HorsePower * laps;
    }
}
