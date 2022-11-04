namespace MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Interfaces;

    public class Commando : SpecialisedSoldier, ICommando
    {
        private IReadOnlyCollection<IMission> missions;

        public Commando(int id, string firstName, string lastName, decimal salary, string corps, ISet<IMission> missions)
            : base(id, firstName, lastName, salary, corps)
        {
            this.missions = new HashSet<IMission>(missions);            
        }

        public IReadOnlyCollection<IMission> Missions => (IReadOnlyCollection<IMission>)missions;

        public void CompleteMission(string codeName)
        {
            Missions.FirstOrDefault(m => m.CodeName == codeName).CompleteMission();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                 .AppendLine("Missions:");

            foreach (var mission in Missions)
            {
                sb.AppendLine($"  {mission}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
