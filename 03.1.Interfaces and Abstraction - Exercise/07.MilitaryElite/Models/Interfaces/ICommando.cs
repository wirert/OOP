namespace MilitaryElite.Models.Interfaces
{
    using System.Collections.Generic;

    public interface ICommando
    {
        public IReadOnlyCollection<IMission> Missions { get; }

        public void CompleteMission(string codeName);
    }
}
