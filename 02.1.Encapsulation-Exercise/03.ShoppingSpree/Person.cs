using System;
using System.Collections.Generic;

namespace _03.ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            products = new List<Product>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }

                name = value;
            }
        }
        public decimal Money
        {
            get { return money; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }

                money = value;
            }
        }
        public IReadOnlyCollection<Product> Products { get { return products.AsReadOnly(); } }

        public void BuyProduct(Product product)
        {
            if (product.Cost > money)
            {
                Console.WriteLine($"{this.name} can't afford {product}");
            }
            else
            {
                Console.WriteLine($"{Name} bought {product}");
                products.Add(product);
                money -= product.Cost;
            }           
        }

    }
}
