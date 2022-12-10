namespace ChristmasPastryShop.Core
{
    using System;
    using System.Linq;

    using Contracts;
    using Models.Booths;
    using Models.Booths.Contracts;
    using Models.Cocktails;
    using Models.Cocktails.Contracts;
    using Models.Delicacies;
    using Models.Delicacies.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;

            Booth boothToAdd = new Booth(boothId, capacity);
            booths.AddModel(boothToAdd);

            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            IDelicacy delicacy = null;

            if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            var booth = booths.Models.First(b => b.BoothId == boothId);

            if (booth.DelicacyMenu.Models.Any(d => d.Name == delicacyName))
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            booth.DelicacyMenu.AddModel(delicacy);

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            ICocktail cocktail = null;

            if (cocktailTypeName == "Hibernation")
            {
                if (size != "Small" && size != "Middle" && size != "Large")
                {
                    return string.Format(OutputMessages.InvalidCocktailSize, size);
                }

                cocktail = new Hibernation(cocktailName, size);
            }
            else if (cocktailTypeName == "MulledWine")
            {
                if (size != "Small" && size != "Middle" && size != "Large")
                {
                    return string.Format(OutputMessages.InvalidCocktailSize, size);
                }

                cocktail = new MulledWine(cocktailName, size);
            }
            else
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            var booth = booths.Models.First(b => b.BoothId == boothId);

            if (booth.CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            booth.CocktailMenu.AddModel(cocktail);

            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            var booth = booths.Models
                .Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId).FirstOrDefault();

            if (booth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            booth.ChangeStatus();

            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            var booth = booths.Models.First(b => b.BoothId == boothId);

            string[] orderArgs = order.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string itemTypeName = orderArgs[0];
            string itemName = orderArgs[1];
            int countOrderedPieces = int.Parse(orderArgs[2]);

            if (itemTypeName == "MulledWine" || itemTypeName == "Hibernation")
            {
                string size = orderArgs[3];

                var item = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == size);

                if (item == null)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, size, itemName);
                }

                booth.UpdateCurrentBill(countOrderedPieces * item.Price);
            }
            else if (itemTypeName == "Gingerbread" || itemTypeName == "Stolen")
            {
                var item = booth.DelicacyMenu.Models.FirstOrDefault(d => d.Name == itemName);

                if (item == null)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                }

                booth.UpdateCurrentBill(countOrderedPieces * item.Price);

            }
            else
            {
               return $"{itemTypeName} is not recognized type!";
            }

            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOrderedPieces, itemName);
        }

        public string LeaveBooth(int boothId)
        {
            var booth = booths.Models.First(b => b.BoothId == boothId);
            double currBill = booth.CurrentBill;

            booth.Charge();
            booth.ChangeStatus();

            return $"Bill {currBill:f2} lv" + Environment.NewLine + $"Booth {boothId} is now available!";
        }

        public string BoothReport(int boothId)
        {
            var booth = booths.Models.First(b => b.BoothId == boothId);

            return booth.ToString();
        }
    }
}
