using System;

namespace WildFarm.Models.Animals
{
    public class Tiger : Feline
    {
        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        { }

        public override string AskForFood() => "ROAR!!!";
        

        protected override void GiveFoodToAnimal(BaseFood food)
        {
            if (food.Type == "Meat")
            {
                FoodEaten += food.Quantity;
                IncreaseWeight(food.Quantity);
            }
            else
            {
                Console.WriteLine($"Tiger does not eat {food.Type}!");
            }
        }
    }
}
