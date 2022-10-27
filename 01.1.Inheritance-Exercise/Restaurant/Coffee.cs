namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        public Coffee(string name, double caffeine) : base(name, 0, 0)
        {
            Caffeine = caffeine;
            Milliliters = 50;
            Price = 3.50M;        
        }

        public double CoffeeMilliliters { get => Milliliters; }
       public decimal CoffeePrice { get => Price; }
        public double Caffeine { get; set; }
    }
}
