namespace WildFarm.Models
{
    public abstract class BaseFood
    {
        protected BaseFood(int quantity)
        {
            Quantity = quantity;            
        }

        public int Quantity { get; private set; }
        public string Type => this.GetType().Name;
    }
}
