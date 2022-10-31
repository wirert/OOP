using System;

namespace _04.PizzaCalories
{
    public class Topping
    {
        private const double BASE_CALORIES = 2;
        private const double MEAT_CALORIES = 1.2;
        private const double VEGGIES_CALORIES = 0.8;
        private const double CHEESE_CALORIES = 1.1;
        private const double SAUCE_CALORIES = 0.9;

        private string type;
        private int weight;
        private double caloriesPerGram;

        public Topping(string type, int weight)
        {
            caloriesPerGram = BASE_CALORIES;
            Type = type;
            Weight = weight;
        }

        private string Type
        {
            set
            {
                switch (value.ToLower())
                {
                    case "meat":
                        caloriesPerGram *= MEAT_CALORIES;
                        break;
                    case "veggies":
                        caloriesPerGram *= VEGGIES_CALORIES;
                        break;
                    case "cheese":
                        caloriesPerGram *= CHEESE_CALORIES;
                        break;
                    case "sauce":
                        caloriesPerGram *= SAUCE_CALORIES;
                        break;
                    default: throw new Exception($"Cannot place {value} on top of your pizza.");
                }

                type = value;
            }
        }
        private int Weight
        {
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new Exception($"{type} weight should be in the range[1..50].");
                }

                weight = value;
            }
        }
        public double CaloriesPerGram => caloriesPerGram;

        public double GetCalories() => weight * caloriesPerGram;
    }
}
