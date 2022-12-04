namespace WarCroft.Entities.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Items;
    using WarCroft.Constants;

    public abstract class Bag : IBag
    {
        private ICollection<Item> items;

        public Bag(int capacity = 100)
        {
            Capacity = capacity;
            items = new HashSet<Item>();
        }

        public int Capacity { get; set; }

        public int Load => Items.Sum(i => i.Weight);

        public IReadOnlyCollection<Item> Items => items as IReadOnlyCollection<Item>;

        public void AddItem(Item item)
        {
            if (this.Load + item.Weight > this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }

            items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (!items.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            var item = items.FirstOrDefault(i => i.GetType().Name == name);

            if (item == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            items.Remove(item);

            return item;
        }
    }
}
