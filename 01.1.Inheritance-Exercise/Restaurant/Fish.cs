namespace Restaurant
{
    public class Fish : MainDish
    {
        public Fish(string name, decimal price) : base(name, price, 0) 
        {
            this.Grams = 22;
        }
    }
}
