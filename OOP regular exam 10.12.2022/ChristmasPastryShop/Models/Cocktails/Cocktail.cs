namespace ChristmasPastryShop.Models.Cocktails
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public abstract class Cocktail : ICocktail
    {
        private string name;
        private double price;

        public Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }

                name = value;
            }
        }
        public string Size { get; private set; }

        public double Price
        {
            get => price;
            private set
            {
                switch (Size)
                {

                    case "Small":
                        price = value / 3;
                        break;
                    case "Middle":
                        price = value * 2 / 3;
                        break;
                    default:
                        price = value;
                        break;
                }
            }
        }

        public override string ToString()
            => $"{Name} ({Size}) - {Price:f2} lv";
    }
}
