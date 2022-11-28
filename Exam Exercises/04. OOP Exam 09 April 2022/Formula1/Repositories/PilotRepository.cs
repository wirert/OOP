namespace Formula1.Repositories
{
    using System.Linq;
    using Models.Contracts;

    public class PilotRepository : Repository<IPilot>
    {     
        public override IPilot FindByName(string name) => Models.FirstOrDefault(p => p.FullName == name);
    }
}
