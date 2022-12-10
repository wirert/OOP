namespace ChristmasPastryShop.Models.Booths
{
    using System;
    using System.Text;

    using Cocktails.Contracts;
    using Contracts;
    using Delicacies.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Booth : IBooth
    {
        private int capacity;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity= capacity;
            DelicacyMenu = new DelicacyRepository();
            CocktailMenu = new CocktailRepository();
        }

        public int BoothId { get; private set; }

        public int Capacity 
        { 
            get => capacity; 
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }

                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu { get; private set; }

        public IRepository<ICocktail> CocktailMenu { get; private set; }

        public double CurrentBill { get; private set; }

        public double Turnover { get; private set; }

        public bool IsReserved { get; private set; }

        public void ChangeStatus() => IsReserved = !IsReserved;
        

        public void Charge()
        {
            Turnover += CurrentBill;
            CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount) => CurrentBill += amount;        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {BoothId}")
                .AppendLine($"Capacity: {Capacity}")
                .AppendLine($"Turnover: {Turnover:f2} lv")
                .AppendLine("-Cocktail menu:");

            foreach (var cocktail in CocktailMenu.Models)
            {
                sb.AppendLine("--" + cocktail.ToString());
            }

            sb.AppendLine("-Delicacy menu:");

            foreach (var delicacy in DelicacyMenu.Models)
            {
                sb.AppendLine("--" + delicacy.ToString());
            }

            return sb.ToString().Trim();
        }
    }
}
