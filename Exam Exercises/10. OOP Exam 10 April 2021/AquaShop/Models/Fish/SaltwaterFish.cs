namespace AquaShop.Models.Fish
{
    using System;

    public class SaltwaterFish : Fish
    {
        private const int INITIAL_SIZE = 5;

        public SaltwaterFish(string name, string species, decimal price) : base(name, species, price)
        {
            Size = INITIAL_SIZE;
        }

        public override void Eat() => Size += 2;
    }
}
