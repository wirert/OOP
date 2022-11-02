using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilitaryElite
{
    public class Commando : SpecialisedSoldier
    {
        private List<Mission> missions;

        public Commando(int id, string firstName, string lastName, decimal salary, string corps, List<Mission> missions) : base(id, firstName, lastName, salary, corps)
        {
            this.missions = missions;
        }

       public void CompleteMission(string codeName)
        {
            missions.FirstOrDefault(m => m.CodeName == codeName).CompleteMission();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}")
                 .AppendLine($"Corps: {Corps}")
                 .AppendLine("Missions:");

            foreach (var mission in missions)
            {
                sb.AppendLine(mission.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
