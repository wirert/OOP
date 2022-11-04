namespace MilitaryElite.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    using Interfaces;

    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        private IReadOnlyCollection<IPrivate> privates;

        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, ISet<IPrivate> privates)
            : base(id, firstName, lastName, salary)
        {
            this.privates = new HashSet<IPrivate>(privates);
        }

        public IReadOnlyCollection<IPrivate> Privates => (IReadOnlyCollection<IPrivate>)privates;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString())
                .AppendLine("Privates:");

            foreach (var soldier in Privates)
            {
                sb.AppendLine($"  {soldier}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
