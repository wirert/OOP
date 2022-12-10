namespace ChristmasPastryShop.Models.Cocktails
{
    public class MulledWine : Cocktail
    {
        private const double Large_MulledWine_Price = 13.5;

        public MulledWine(string cocktailName, string size) 
            : base(cocktailName, size, Large_MulledWine_Price)
        {
        }
    }
}
