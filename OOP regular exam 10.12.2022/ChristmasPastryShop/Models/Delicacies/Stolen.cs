namespace ChristmasPastryShop.Models.Delicacies
{
    public class Stolen : Delicacy
    {
        private const double Stolen_Price = 3.5;

        public Stolen(string delicacyName) : base(delicacyName, Stolen_Price)
        {
        }
    }
}
