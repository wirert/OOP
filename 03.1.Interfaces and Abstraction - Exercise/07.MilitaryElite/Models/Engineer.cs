namespace MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Text;

    using Interfaces;

    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private IReadOnlyCollection<IRepair> repairs;

        public Engineer(int id, string firstName, string lastName, decimal salary, string corps, ISet<IRepair> repairs) 
            : base(id, firstName, lastName, salary, corps)
        {
            this.repairs = new HashSet<IRepair>(repairs);
        }

        public IReadOnlyCollection<IRepair> Repairs => (IReadOnlyCollection<IRepair>)repairs;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())                 
                 .AppendLine("Repairs:");

            foreach (var repair in Repairs)
            {
                sb.AppendLine($"  {repair}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
