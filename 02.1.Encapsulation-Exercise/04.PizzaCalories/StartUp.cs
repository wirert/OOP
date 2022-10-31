using System;
using System.Collections.Generic;
using System.Data;

namespace _04.PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] pizzaInfo = Console.ReadLine().Split(" ");
            string[] doughInfo = Console.ReadLine().Split(" ");
            try
            {
                Dough dough = new Dough(doughInfo[1], doughInfo[2], int.Parse(doughInfo[3]));

                Pizza pizza = new Pizza(pizzaInfo[1], dough);
                
                string command = null;

                while ((command = Console.ReadLine()) != "END")
                {
                    string[] toppingInfo = command.Split(" ");
                    
                    pizza.AddTopping(new Topping(toppingInfo[1], int.Parse(toppingInfo[2])));
                }                

                Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories:F2} Calories.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }           
        }
                
    }
}
