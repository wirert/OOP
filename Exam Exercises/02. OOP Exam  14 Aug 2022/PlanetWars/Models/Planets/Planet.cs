namespace PlanetWars.Models.Planets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Contracts;
    using MilitaryUnits;
    using MilitaryUnits.Contracts;
    using PlanetWars.Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;
    using Weapons;
    using Weapons.Contracts;
    

    public class Planet : IPlanet
    {
        private IRepository<IMilitaryUnit> units;
        private IRepository<IWeapon> weapons;
        private string name;
        private double budget;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            units = new UnitRepository();
            this.weapons = new WeaponRepository();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }

                name = value;
            }
        }

        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }

                budget = value;
            }
        }

        public double MilitaryPower => CalculateMilitaryPower();

        public IReadOnlyCollection<IMilitaryUnit> Army => units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit) => units.AddItem(unit);

        public void AddWeapon(IWeapon weapon) => weapons.AddItem(weapon);


        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {this.Name}")
                .AppendLine($"--Budget: {this.Budget} billion QUID");

            string forces = this.Army.Count == 0 ? "No units" : string.Join( ", ", this.Army );
            sb.AppendLine("--Forces: " + forces);

            string equipment = this.Weapons.Count == 0 ? "No weapons" : string.Join( ", ", this.Weapons );
            sb.AppendLine("--Combat equipment: " + equipment)
                .AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().Trim();
        }

        public void Profit(double amount) => this.Budget += amount;        

        public void Spend(double amount)
        {
            if (this.Budget < amount)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var unit in this.Army)
            {
                unit.IncreaseEndurance();
            }
        }

        private double CalculateMilitaryPower()
        {
            int totalEndurance = this.Army.Sum(u => u.EnduranceLevel);
            var totalDestructionLevel = this.Weapons.Sum(w => w.DestructionLevel);

            double totalPower = totalEndurance + totalDestructionLevel;

            if (this.Army.Any(u => u.GetType().Name == typeof(AnonymousImpactUnit).Name)) 
            {
                totalPower *= 1.3;
            }

            if (this.Weapons.Any(w => w.GetType().Name == typeof(NuclearWeapon).Name)) 
            {
                totalPower *= 1.45;
            }

            return Math.Round(totalPower, 3);
        }
    }
}