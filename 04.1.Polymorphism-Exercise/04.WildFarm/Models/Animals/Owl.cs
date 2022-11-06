using System;

namespace WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override string AskForFood() => "Hoot Hoot";       

        protected override void GiveFoodToAnimal(BaseFood food)
        {
            if (food.Type == "Meat")
            {
                FoodEaten += food.Quantity;
                IncreaseWeight(0.25 * food.Quantity);
            }
            else
            {
                Console.WriteLine($"Owl does not eat {food.Type}!");
            }
        }
    }
}
