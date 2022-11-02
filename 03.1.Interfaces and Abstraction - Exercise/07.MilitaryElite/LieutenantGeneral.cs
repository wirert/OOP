using System.Collections.Generic;
using System.Text;

namespace MilitaryElite
{
    public class LieutenantGeneral : Private
    {
        private List<Private> privates = new List<Private>();

        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, List<Private> privates) : base(id, firstName, lastName, salary)
        {
            this.privates = privates;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}")
                .AppendLine("Privates:");

            foreach (var soldier in privates)
            {
                sb.AppendLine(soldier.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
