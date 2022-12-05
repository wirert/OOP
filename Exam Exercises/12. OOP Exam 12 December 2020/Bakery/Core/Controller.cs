namespace Bakery.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Bakery.Models.BakedFoods;
    using Bakery.Models.Drinks;
    using Bakery.Models.Tables;
    using Bakery.Utilities.Messages;
    using Contracts;
    using Models.BakedFoods.Contracts;
    using Models.Drinks.Contracts;
    using Models.Tables.Contracts;

    public class Controller : IController
    {
        private ICollection<IBakedFood> bakedFoods;
        private ICollection<IDrink> drinks;
        private ICollection<ITable> tables;
        private decimal totalIncome;

        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();
        }

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food = null;

            if (type == "Cake")
            {
                food = new Cake(name, price);
            }
            else if (type == "Bread")
            {
                food = new Bread(name, price);
            }
            else
            {
                return null;
            }

            bakedFoods.Add(food);

            return string.Format(OutputMessages.FoodAdded, name, type);
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink = null;

            if (type == "Tea")
            {
                drink = new Tea(name, portion, brand);
            }
            else if (type == "Water")
            {
                drink = new Water(name, portion, brand);
            }
            else
            {
                return null;
            }

            drinks.Add(drink);

            return string.Format(OutputMessages.DrinkAdded, name, brand);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = null;

            if (type == "InsideTable")
            {
                table = new InsideTable(tableNumber, capacity);
            }
            else if (type == "OutsideTable")
            {
                table = new OutsideTable(tableNumber, capacity);
            }
            else
            {
                return null;
            }

            tables.Add(table);

            return string.Format(OutputMessages.TableAdded, tableNumber);
        }

        public string ReserveTable(int numberOfPeople)
        {
            var table = tables.FirstOrDefault(t => t.Capacity >= numberOfPeople && !t.IsReserved);

            if (table == null)
            {
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }

            table.Reserve(numberOfPeople);

            return string.Format(OutputMessages.TableReserved, table.TableNumber, numberOfPeople);
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            var food = bakedFoods.FirstOrDefault(f => f.Name == foodName);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            if (food == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }

            table.OrderFood(food);

            return string.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            var drink = drinks.FirstOrDefault(d => d.Name == drinkName && d.Brand == drinkBrand);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            if (drink == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }

            table.OrderDrink(drink);            

            return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
        }

        public string LeaveTable(int tableNumber)
        {
            var table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

            if (table == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            decimal bill = table.GetBill();
            totalIncome += bill;
            table.Clear();

            return $"Table: {tableNumber}" + Environment.NewLine + $"Bill: {bill:f2}";
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var table in tables.Where(t => !t.IsReserved))
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }

            return sb.ToString().Trim();
        }

        public string GetTotalIncome() => string.Format(OutputMessages.TotalIncome, totalIncome);
    }
}
