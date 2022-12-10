namespace ChristmasPastryShop.Models.Cocktails
{
    public class Hibernation : Cocktail
    {
        private const double Large_Hibernation_Price = 10.5;

        public Hibernation(string cocktailName, string size)
            : base(cocktailName, size, Large_Hibernation_Price)
        {
        }
    }
}
