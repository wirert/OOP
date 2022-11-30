namespace SpaceStation.Models.Bags
{
    using SpaceStation.Models.Bags.Contracts;
    using System.Collections.Generic;

    public class Backpack : IBag
    {
        public Backpack()
        {
            Items= new List<string>();
        }

        public ICollection<string> Items { get; private set; }
    }
}
