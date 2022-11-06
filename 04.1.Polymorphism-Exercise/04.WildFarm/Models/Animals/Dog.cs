using System;

namespace WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override string AskForFood() => "Woof!";
       

        protected override void GiveFoodToAnimal(BaseFood food)
        {
            if (food.Type == "Meat")
            {
                FoodEaten += food.Quantity;
                IncreaseWeight(0.4 * food.Quantity);
            }
            else
            {
                Console.WriteLine($"Dog does not eat {food.Type}!");
            }
        }
    }
}
