namespace Gym.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Athletes;
    using Models.Equipment;
    using Models.Equipment.Contracts;
    using Models.Gyms;
    using Models.Gyms.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private IRepository<IEquipment> equipment;
        private List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym = null;

            if (gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            gyms.Add(gym);

            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment = null;

            if (equipmentType == "BoxingGloves")
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment = new Kettlebell();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            this.equipment.Add(equipment);

            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IGym gym = FindGymByName(gymName);

            if (athleteType == "Boxer")
            {
                if (gym.GetType().Name == "BoxingGym")
                {
                    gym.AddAthlete(new Boxer(athleteName, motivation, numberOfMedals));
                }
                else
                {
                    return OutputMessages.InappropriateGym;
                }
            }
            else if (athleteType == "Weightlifter")
            {
                if (gym.GetType().Name == "WeightliftingGym")
                {
                    gym.AddAthlete(new Weightlifter(athleteName, motivation, numberOfMedals));
                }
                else
                {
                    return OutputMessages.InappropriateGym;
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var gym = FindGymByName(gymName);

            IEquipment equip = this.equipment.FindByType(equipmentType);

            if (equip == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            gym.AddEquipment(equip);
            this.equipment.Remove(equip);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string TrainAthletes(string gymName)
        {
            var gym = FindGymByName(gymName);

            gym.Exercise();

            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }

        public string EquipmentWeight(string gymName)
            => string.Format(OutputMessages.EquipmentTotalWeight, gymName, FindGymByName(gymName).EquipmentWeight);

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().Trim();
        }

        private IGym FindGymByName(string gymName) => gyms.First(g => g.Name == gymName);
    }
}
