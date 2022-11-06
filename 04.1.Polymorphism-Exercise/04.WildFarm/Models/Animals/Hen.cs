using System;
using System.Collections.Generic;
using System.Text;

namespace WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
        { }

        public override string AskForFood() => "Cluck";
        

        protected override void GiveFoodToAnimal(BaseFood food)
        {
            FoodEaten += food.Quantity;
            IncreaseWeight(0.35 * food.Quantity);
        }
    }
}
