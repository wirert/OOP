namespace Gym.Models.Gyms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Athletes.Contracts;
    using Contracts;
    using Equipment.Contracts;
    using Utilities.Messages;

    public abstract class Gym : IGym
    {
        private string name;
        private List<IEquipment> equipment;
        private List<IAthlete> athletes;

        protected Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            equipment = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public double EquipmentWeight => Equipment.Sum(e => e.Weight);

        public ICollection<IEquipment> Equipment => equipment.AsReadOnly();

        public ICollection<IAthlete> Athletes => athletes.AsReadOnly();

        public void AddAthlete(IAthlete athlete)
        {
            if (Athletes.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }

            athletes.Add(athlete);
        }

        public bool RemoveAthlete(IAthlete athlete) => athletes.Remove(athlete);

        public void AddEquipment(IEquipment equipment) => this.equipment.Add(equipment);

        public void Exercise()
        {
            foreach (var athlet in athletes)
            {
                athlet.Exercise();
            }
        }

        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            string athlets = Athletes.Count == 0 ? "No athletes" : string.Join(", ", Athletes);

            sb.AppendLine($"{this.Name} is a {this.GetType().Name}:")
                .AppendLine("Athletes: " + athlets)
                .AppendLine($"Equipment total count: {this.Equipment.Count}")
                .AppendLine($"Equipment total weight: {this.EquipmentWeight} grams");

            return sb.ToString().Trim();
        }
    }
}
