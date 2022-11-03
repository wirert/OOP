using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilitaryElite
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(int id, string firstName, string lastName, decimal salary, string corps, List<string> missions) : base(id, firstName, lastName, salary, corps)
        {
            this.Missions = new List<Mission>();
            for (int i = 0; i < missions.Count - 1; i += 2)
            {
                string missionName = missions[i];
                string missionState = missions[i + 1];
                if (missionState == "inProgress" || missionState == "Finished")
                    this.Missions.Add(new Mission(missionName, missionState));
            }
        }

        public List<Mission> Missions { get; set; }

        public void CompleteMission(string codeName)
        {
            Missions.FirstOrDefault(m => m.CodeName == codeName).CompleteMission();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}")
                 .AppendLine($"Corps: {Corps}")
                 .AppendLine("Missions:");

            foreach (var mission in Missions)
            {
                sb.AppendLine($"  {mission}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
