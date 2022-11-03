using System.Collections.Generic;
using System.Text;

namespace MilitaryElite
{
    public class LieutenantGeneral : Private, ILieutenantGeneral
    {       
        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, List<Private> privates) : base(id, firstName, lastName, salary)
        {
            this.Privates = privates;
        }

        public List<Private> Privates { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}")
                .AppendLine("Privates:");

            foreach (var soldier in Privates)
            {
                sb.AppendLine($"  {soldier}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
