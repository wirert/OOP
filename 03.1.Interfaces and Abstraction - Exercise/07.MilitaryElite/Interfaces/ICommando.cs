using System.Collections.Generic;

namespace MilitaryElite
{
    public interface ICommando
    {
        public List<Mission> Missions { get; set; }

        public void CompleteMission(string codeName);
    }
}
