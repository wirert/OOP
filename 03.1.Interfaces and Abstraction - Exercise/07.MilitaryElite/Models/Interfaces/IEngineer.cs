namespace MilitaryElite.Models.Interfaces
{
    using System.Collections.Generic;

    public interface IEngineer
    {
        public IReadOnlyCollection<IRepair> Repairs { get; }
    }
}
