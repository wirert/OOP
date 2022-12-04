namespace WarCroft.Entities.Characters.Contracts
{
    using System;

    using Constants;
    using Inventory;
    using Items;

    public abstract class Character
    {
        private string name;
        private double health;
        private double armor;        

        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
            Name = name;
            BaseHealth = health;
            Health = BaseHealth;
            BaseArmor = armor;
            Armor = BaseArmor;
            AbilityPoints = abilityPoints;
            Bag = bag;
            IsAlive = true;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }

                name = value;
            }
        }
        public double Health
        {
            get => health;
            set
            {
                if (value <= 0)
                {
                    IsAlive = false;
                    health = 0;
                }
                else if (value >= BaseHealth)
                {
                    health = BaseHealth;
                }
                else
                {
                    health = value;
                }
            }
        }
        public double Armor
        {
            get => armor;
            private set
            {
                if (value <= 0)
                {
                    armor = 0;
                }
                else
                {
                    armor = value;
                }
            }
        }
        public double AbilityPoints { get; private set; }
        public IBag Bag { get; private set; }
        public bool IsAlive { get; private set; }
        public double BaseHealth { get; private set; }
        public double BaseArmor { get; private set; }

        public void EnsureAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }
        }

        public void TakeDamage(double hitPoints)
        {
            EnsureAlive();

            if (Armor > hitPoints)
            {
                Armor -= hitPoints;
            }
            else
            {
                hitPoints -= Armor;
                Armor = 0;

                Health -= hitPoints;
            }
        }

        public void UseItem(Item item) => item.AffectCharacter(this);

        public override string ToString()
                => $"{Name} - HP: {Health}/{BaseHealth}, AP: {Armor}/{BaseArmor}, Status: {(IsAlive ? "Alive" : "Dead")}";
    }
}