namespace WildFarm.Models
{
    public abstract class Animal
    {
        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; private set; }
        public double Weight { get; private set; }
        protected int FoodEaten { get; set; }

        public abstract string AskForFood();

        protected abstract void GiveFoodToAnimal(BaseFood food);

        public void FeedAnimal(BaseFood food)
        {
            GiveFoodToAnimal(food);
        }

        protected void IncreaseWeight(double weight) => this.Weight += weight;

        public override string ToString()
        {
            return $"{GetType().Name} [{this.Name}, ";
        }
    }
}
