﻿namespace WildFarm
{
    using System;
    using System.Collections.Generic;

    using Models;
    using Models.Animals;
    using WildFarm.Models.Foods;

    public class StartUp
    {
        static void Main(string[] args)
        {
            HashSet<Animal> animals = new HashSet<Animal>();

            string command = null;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] animalInfo = command.Split();

                Animal animal = ReadAnimal(animalInfo);
                animals.Add(animal);

                BaseFood food = ReadFood();

                Console.WriteLine(animal.AskForFood());

                animal.FeedAnimal(food);
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }

        private static Animal ReadAnimal(string[] animalInfo)
        {
            string type = animalInfo[0];
            string name = animalInfo[1];
            double weight = double.Parse(animalInfo[2]);

            Animal animal = null;

            switch (type)
            {
                case "Hen":
                    double wingSize = double.Parse(animalInfo[3]);
                    animal = new Hen(name, weight, wingSize);
                    break;
                case "Owl":
                    double wingSizeOwl = double.Parse(animalInfo[3]);
                    animal = new Owl(name, weight, wingSizeOwl);
                    break;
                case "Mouse":
                    string mouseRegion = animalInfo[3];
                    animal = new Mouse(name, weight, mouseRegion);
                    break;
                case "Dog":
                    string dogRegion = animalInfo[3];
                    animal = new Dog(name, weight, dogRegion);
                    break;
                case "Cat":
                    string catRegion = animalInfo[3];
                    string catBreed = animalInfo[4];
                    animal = new Cat(name, weight, catRegion, catBreed);
                    break;
                case "Tiger":
                    string tigerRegion = animalInfo[3];
                    string tigerBreed = animalInfo[4];
                    animal = new Tiger(name, weight, tigerRegion, tigerBreed);
                    break;
            }

            return animal;
        }

        private static BaseFood ReadFood()
        {
            string[] foodInfo = Console.ReadLine().Split();

            string type = foodInfo[0];
            int quantity = int.Parse(foodInfo[1]);

            BaseFood food = null;

            switch (type)
            {
                case "Fruit":
                    food  = new Fruit(quantity);
                    break;
                case "Meat":
                    food = new Meat(quantity);
                    break;
                case "Seeds":
                    food = new Seeds(quantity);
                    break;
                case "Vegetable":
                    food = new Vegetable(quantity);
                    break;
            }

            return food;
        }
    }
}
