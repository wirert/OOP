using System.Collections.Generic;
using System.Text;

namespace MilitaryElite
{
    public class Engineer : SpecialisedSoldier
    {
        private Dictionary<string, int> repairs;

        public Engineer(int id, string firstName, string lastName, decimal salary, string corps, Dictionary<string, int> repairs) : base(id, firstName, lastName, salary, corps)
        {
            this.repairs = repairs;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {FirstName} {LastName} Id: {Id} Salary: {Salary:f2}")
                 .AppendLine($"Corps: {Corps}")
                 .AppendLine("Repairs:");

            foreach (var repair in repairs)
            {
                sb.AppendLine($"Part Name: {repair.Key} Hours Worked: {repair.Value}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
