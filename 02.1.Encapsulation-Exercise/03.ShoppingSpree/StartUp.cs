using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Person> buyers = new List<Person>();
            List<Product> products = new List<Product>();
            
            try
            {
                buyers = ReadBuyers();
                products = ReadProducts();
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
                return;
            }

            string command = null;

            while ((command = Console.ReadLine()) != "END")
            {
                string[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string buyerName = tokens[0];
                string productName = tokens[1];

                Person buyer = buyers.FirstOrDefault(b => b.Name == buyerName);
                Product product = products.FirstOrDefault(p => p.Name == productName);

                if (buyer != null && product != null)
                {
                    buyer.BuyProduct(product);
                }
            }

            foreach (var buyer in buyers)
            {
                string boughtProducts = buyer.Products.Count == 0
                    ? "Nothing bought"
                    : $"{string.Join(", ", buyer.Products)}";

                Console.WriteLine($"{buyer.Name} - {boughtProducts}");
            }
        }

        private static List<Person> ReadBuyers()
        {
            List<Person> buyers = new List<Person>();

            string[] inputPersons = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in inputPersons)
            {
                string[] info = item.Split('=');

                if (!decimal.TryParse(info[1], out decimal money))
                    money = -1;

                buyers.Add(new Person(info[0], money));
            }

            return buyers;
        }

        private static List<Product> ReadProducts()
        {
            List<Product> products = new List<Product>();

            string[] inputProducts = Console.ReadLine()
               .Split(";", StringSplitOptions.RemoveEmptyEntries);                        

            foreach (var item in inputProducts)
            {
                string[] info = item.Split('=');

                if (!decimal.TryParse(info[1], out decimal money))
                    money = -1;

                products.Add(new Product(info[0], money));
            }

            return products;
        }
    }
}
