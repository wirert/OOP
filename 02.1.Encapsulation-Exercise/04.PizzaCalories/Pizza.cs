using System;
using System.Collections.Generic;

namespace _04.PizzaCalories
{
    public class Pizza
    {
        private string name;
        private List<Topping> toppings;
        private Dough dough;

        public Pizza(string name, Dough dough)
        {
            this.Name = name;
            this.dough = dough;
            toppings = new List<Topping>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 15)
                {
                    throw new Exception("Pizza name should be between 1 and 15 symbols.");
                }

                name = value;
            }
        }       
        public Dough Dough { get { return dough; } set { dough = value; } }
        public int NumberOfToppings => toppings.Count;
        public double TotalCalories
        {
            get
            {
                double total = dough.GetCalories();

                foreach (var topping in toppings)
                {
                    total += topping.GetCalories();
                }

                return total;
            }
        }

        public void AddTopping(Topping topping)
        {
            if(NumberOfToppings == 10) throw new Exception("Number of toppings should be in range [0..10].");

            toppings.Add(topping);
        }
    }
}
