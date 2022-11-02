using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodShortage
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int numPeople = int.Parse(Console.ReadLine());

            List<IBuyer> buyers = new List<IBuyer>();

            for (int i = 0; i < numPeople; i++)
            {
                string[] personInfo = Console.ReadLine().Split();
                string name = personInfo[0];
                int age = int.Parse(personInfo[1]);

                if (personInfo.Length == 3)
                {
                    IBuyer rebel = new Rebel(name, age, personInfo[2]);
                    buyers.Add(rebel);
                }
                else if (personInfo.Length == 4)
                {
                    IBuyer citizen = new Citizen(name, age, personInfo[2], personInfo[3]);
                    buyers.Add(citizen);
                }
            }

            string buyerName;

            while ((buyerName = Console.ReadLine()) != "End")
            {
                var buyer = buyers.FirstOrDefault(x => x.Name == buyerName);

                if (buyer != null)
                {
                    buyer.BuyFood();
                }
            }

            int allFood = buyers.Sum(b => b.Food);

            Console.WriteLine(allFood);
        }
    }
}
