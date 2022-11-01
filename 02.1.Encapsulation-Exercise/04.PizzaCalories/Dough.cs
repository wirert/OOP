using System;

namespace _04.PizzaCalories
{
    public class Dough
    {
        private const double WHITE_CALORIES = 1.5;
        private const double WHOLEGRAIN_CALORIES = 1;
        private const double CRYSPY_CALORIES = 0.9;
        private const double CHEWY_CALORIES = 1.1;
        private const double HOMEMADE_CALORIES = 1;

        private string flourType;
        private string bakingTechnique;
        private int weight;        
        private double doughCalories;
        private double calorieModifier;

        public Dough(string flourType, string bakingTechnique, int weight)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public double CaloriesPerGram => 2 * doughCalories * calorieModifier;

        private string FlourType
        {            
            set
            {
                if (value.ToLower() == "white") doughCalories = WHITE_CALORIES;
                else if (value.ToLower() == "wholegrain") doughCalories = WHOLEGRAIN_CALORIES;
                else throw new Exception("Invalid type of dough.");

                flourType = value;
            }
        }
        private string BakingTechnique
        {            
            set
            {
                if (value.ToLower() == "crispy") calorieModifier = CRYSPY_CALORIES;
                else if (value.ToLower() == "chewy") calorieModifier = CHEWY_CALORIES;
                else if (value.ToLower() == "homemade") calorieModifier = HOMEMADE_CALORIES;
                else throw new Exception("Invalid type of dough.");

                bakingTechnique = value;
            }
        }
        private int Weight
        {            
            set
            {
                if (value < 1 || value > 200)
                {
                    throw new Exception("Dough weight should be in the range [1..200].");
                }

                weight = value;
            }
        }

        public double GetCalories() => weight * CaloriesPerGram;   
    }
}
