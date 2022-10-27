namespace Restaurant
{
    public class Cake : Dessert
    {
        public Cake(string name) : base(name, 0, 0, 0)
        {
            Grams = 250;
            Calories = 1000;
            Price = 5M;
        }

        public decimal CakePrice { get => Price; }
    }
}
