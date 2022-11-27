namespace Heroes.Models.Weapons
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;

        protected Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidWeaponName);
                }

                name = value;
            }
        }

        public int Durability
        {
            get => this.durability;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidWeaponDurability);
                }

                durability = value;
            }
        }

        public abstract int DoDamage();       
    }
}
