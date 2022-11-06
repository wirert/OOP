using System;

namespace WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        { }

        public override string AskForFood() => "Squeak";
        
        protected override void GiveFoodToAnimal(BaseFood food)
        {
            if (food.Type == "Vegetable" || food.Type == "Fruit")
            {
                FoodEaten += food.Quantity;
                IncreaseWeight(0.1 * food.Quantity);
            }
            else
            {
                Console.WriteLine($"Mouse does not eat {food.Type}!");
            }
        }
    }
}
