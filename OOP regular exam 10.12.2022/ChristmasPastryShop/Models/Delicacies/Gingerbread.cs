namespace ChristmasPastryShop.Models.Delicacies
{
    public class Gingerbread : Delicacy
    {
        private const double Gignerbread_Price = 4;

        public Gingerbread(string delicacyName) : base(delicacyName, Gignerbread_Price)
        {
        }
    }
}
