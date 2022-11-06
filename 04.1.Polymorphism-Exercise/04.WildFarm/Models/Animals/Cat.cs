using System;

namespace WildFarm.Models.Animals
{
    internal class Cat : Feline
    {
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        { }

        public override string AskForFood() => "Meow";
        

        protected override void GiveFoodToAnimal(BaseFood food)
        {
            if (food.Type == "Vegetable" || food.Type == "Meat")
            {
                FoodEaten += food.Quantity;
                IncreaseWeight(0.3 * food.Quantity);
            }
            else
            {
                Console.WriteLine($"Cat does not eat {food.Type}!");
            }
        }
    }
}
