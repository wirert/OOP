namespace Bakery.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BakedFoods.Contracts;
    using Contracts;
    using Drinks.Contracts;
    using Utilities.Messages;

    public abstract class Table : ITable
    {
        private readonly ICollection<IBakedFood> foodOrders;
        private readonly ICollection<IDrink> drinkOrders;
        private int capacity;
        private int numberOfPeople;

        private Table()
        {
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
        }

        public Table(int tableNumber, int capacity, decimal pricePerPerson) : this()
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson = pricePerPerson;
        }

        public int TableNumber { get; private set; }

        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get => numberOfPeople;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                numberOfPeople = value;
            }
        }

        public decimal PricePerPerson { get; private set; }

        public bool IsReserved => numberOfPeople > 0;

        public decimal Price => PricePerPerson * NumberOfPeople;

        public void Reserve(int numberOfPeople) => NumberOfPeople = numberOfPeople;

        public void OrderFood(IBakedFood food) => foodOrders.Add(food);

        public void OrderDrink(IDrink drink) => drinkOrders.Add(drink);

        public decimal GetBill() => drinkOrders.Sum(d => d.Price) + foodOrders.Sum(f => f.Price) + Price;


        public void Clear()
        {
            drinkOrders.Clear();
            foodOrders.Clear();
            this.numberOfPeople = 0;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Table: {TableNumber}")
                .AppendLine($"Type: {this.GetType().Name}")
                .AppendLine($"Capacity: {Capacity}")
                .AppendLine($"Price per Person: {PricePerPerson}");

            return sb.ToString().Trim();
        }
    }
}
